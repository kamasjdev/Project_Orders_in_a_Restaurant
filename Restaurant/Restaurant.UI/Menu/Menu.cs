using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Restaurant.Infrastructure.Requests;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.ApplicationLogic.DTO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Restaurant.UI
{
    public partial class Menu : UserControl
    {
        private readonly List<ProductDto> productsList = new List<ProductDto>();
        private readonly List<AdditionDto> additionsList = new List<AdditionDto>();
        decimal amountToPay = decimal.Zero;
        private readonly IRequestHandler _requestHandler;
        private IEnumerable<ProductDto> _products = new List<ProductDto>();
        private IEnumerable<AdditionDto> _additions = new List<AdditionDto>();
        private ProductDto currentProduct;
        private AdditionDto currentAddition;
        private readonly Options _options;
        private string email;

        public Menu(IRequestHandler requestHandler, Options options)
        {
            _requestHandler = requestHandler;
            _options = options;
            InitializeComponent();
        }

        private void ChangedItem(object sender, EventArgs e)
        {
            var currentProductName = (string) comboBoxMainDishes1.SelectedItem;

            if (currentProductName is null)
            {
                return;
            }

            currentProduct = _products.Where(p => p.ProductName == currentProductName).SingleOrDefault();

            if (currentProduct is null)
            {
                return;
            }

            currentAddition = null;
            var additions = _additions.Where(a => a.ProductKind == currentProduct.ProductKind).ToList();

            if (additions.Any())
            {
                comboBoxAdditions.Text = "";
                label3.Visible = true;
                comboBoxAdditions.Items.Clear();
                comboBoxAdditions.Items.AddRange(additions.Select(a => a.AdditionName).ToArray());
                comboBoxAdditions.Visible = true;
            }
            else
            {
                label3.Visible = false;
                comboBoxAdditions.Visible = false;
            }
        }

        private void AddToOrder(object sender, EventArgs e)
        {
            var product = _products.Where(p => p.ProductName == (string) comboBoxMainDishes1.SelectedItem).SingleOrDefault();

            if (product != null)
            {
                productsList.Add(product);
                listViewOrderedProducts.Items.Add(product.ToString());
            }

            var addition = _additions.Where(a => a.AdditionName == (string)comboBoxAdditions.SelectedItem).SingleOrDefault();

            if (addition != null)
            {
                additionsList.Add(addition);
                listViewOrderedProducts.Items.Add(addition.ToString());
            }
        }

        private void DeleteFromOrder(object sender, EventArgs e)
        {
            if (listViewOrderedProducts.SelectedItems != null)
            {
                if (listViewOrderedProducts.SelectedIndices.Count <= 0)
                {
                    return;
                }

                else if (listViewOrderedProducts.SelectedIndices.Count >= 1)
                {
                    var result = MessageBox.Show("Czy chcesz usunąć danie", "Usuń danie",
                               MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        for (int i = listViewOrderedProducts.SelectedIndices.Count - 1; i >= 0; i--)
                        {
                            int selectedindex = listViewOrderedProducts.SelectedIndices[i];
                            var item = listViewOrderedProducts.Items[selectedindex].Text;
                            listViewOrderedProducts.Items.RemoveAt(selectedindex);
                            
                            var product = productsList.Where(p => p.ProductName == item).FirstOrDefault();
                            if (product != null)
                            {
                                productsList.Remove(product);
                                continue;
                            }

                            var addition = additionsList.Where(a => a.AdditionName == item).FirstOrDefault();
                            if (addition != null)
                            {
                                additionsList.Remove(addition);
                                continue;
                            }
                        }
                    }
                }
            }
        }

        private void RefreshCost(object sender, EventArgs e) // funkcja wywołana przez timer1 (interwał co 1s)
        {
            if (currentProduct != null)
            {
                var additionPrice = currentAddition != null ? currentAddition.Price : decimal.Zero;
                var amountPrice = currentProduct.Price + additionPrice;
                PriceProduct.Text = $"{amountPrice.WithTwoDecimalPoints()} zł";

                if (amountPrice > 0)
                {
                    PriceProduct.Visible = true;
                    PriceProductLabel.Visible = true;
                }
                else
                {
                    PriceProduct.Visible = false;
                    PriceProductLabel.Visible = false;
                }
            }
            

            amountToPay = decimal.Zero;

            if (productsList != null)
            {
                foreach (ProductDto p in productsList)
                {
                    amountToPay += p.Price;
                }
            }
            else
            {
                amountToPay = decimal.Zero;
            }

            labelCostOfOrder.Text = "Koszt: " + amountToPay.WithTwoDecimalPoints() + "zł";
        }

        private void LoadLeftMenu(object sender, EventArgs e) // funkcja wykrywająca zmianę pozycji Visible
        {
            if (this.Visible == true)
            {
                email = Extensions.ShowDialogEmail("Wprowadź email", "Email", 
                    (emailToValid) =>
                    {
                        if (string.IsNullOrWhiteSpace(emailToValid))
                        {
                            return false;
                        }

                        if (!Regex.Match(emailToValid, Extensions.EMAIL_PATTERN).Success)
                        {
                            return false;
                        }

                        return true;

                    });
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
                productsList.Clear();
            }
        }

        private void OrderRealization(object sender, EventArgs e)  // funkcja realizująca zamówienie, która przesyła zawartość zamówienia na adres email i wstawia wartości do tabeli SQL
        {
            if (productsList.Count != 0) // gdy lista produktów nie jest pusta
            {
                Order order = new Order(new List<Components.Product>())
                    {
                        Email = email
                    };// stwórz zamówienie (obiekt) z listy produktów
                    

                    bool email_sent = MailSender.Email(MailSender.ContentEmail(order), _options, order.Get_Order_Nr()); // prześlij zamówienie na maila

                    if(email_sent)
                    {
                        // dodawanie rekordów do tabeli 
                        using (ConnectionDB_LINQDataContext db = new ConnectionDB_LINQDataContext()) //DataContext źródło wszystkich encji (Produkty, Zamowienia) mapowanych za pośrednictwem połączenia z bazą danych (connectionString)
                        {
                            AppService.InsertDataToZamDatabase(db, order);

                            // przeszukaj tabelę w celu znalezienia id zamówienia
                            var id_order = AppService.SearchForIdInZamDatabase(db, order.Get_Order_Nr());

                            // dla każdego produktu przypisz wartości poszczególnym kolumną               
                            AppService.InsertDataToProdDatabase(db, new List<Components.Product>(), id_order);
                        }
                    }
            }
            else
            {
                MessageBox.Show("Dodaj coś do listy", "Zamówienie",
                                   MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            labelCostOfOrder.Text = amountToPay > 0 ? "Koszt: " + amountToPay + "zł" : "";
            _products = _requestHandler.Send<IProductService, IEnumerable<ProductDto>>(s => s.GetAll());
            _additions = _requestHandler.Send<IAdditonService, IEnumerable<AdditionDto>>(s => s.GetAll());
            comboBoxMainDishes1.Items.AddRange(_products.Select(p => p.ProductName).ToArray());
        }

        private void ChangedAddition(object sender, EventArgs e)
        {
            var currentAdditionName = (string) comboBoxAdditions.SelectedItem;

            if (currentAdditionName is null)
            {
                return;
            }

            currentAddition = _additions.Where(a => a.AdditionName == currentAdditionName).SingleOrDefault();
        }
    }
}
