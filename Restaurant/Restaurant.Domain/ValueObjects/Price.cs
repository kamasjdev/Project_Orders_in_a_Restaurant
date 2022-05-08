using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Domain.ValueObjects
{
    public sealed class Price : IEquatable<Price>
    {
        private const int numbersAfterDot = 4;

        public long WholePart { get; }
        public long FractionalPart { get; }

        private Price(long wholePart, long fractionalPart)
        {
            WholePart = wholePart;
            FractionalPart = fractionalPart;
        }

        public static Price Of(decimal price)
        {
            var priceModified = decimal.Round(price, numbersAfterDot, MidpointRounding.AwayFromZero);
            int wholePart = (int)priceModified;
            long fractionalPart = (long) (priceModified % 1.0m);
            return new Price(wholePart, fractionalPart);
        }

        public static Price Of(int price)
        {
            int wholePart = price;
            long fractionalPart = 0;
            return new Price(wholePart, fractionalPart);
        }

        public static Price operator +(Price price, Price other)
        {
            return new Price(price.WholePart + other.WholePart, price.FractionalPart + other.FractionalPart);
        }

        public static Price operator -(Price price, Price other)
        {
            return new Price(price.WholePart - other.WholePart, price.FractionalPart - other.FractionalPart);
        }

        public bool Equals(Price other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj as Price == null) return false;
            return Equals((Price)obj);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        private IEnumerable<object> GetEqualityComponents()
        {
            yield return WholePart;
            yield return FractionalPart;
        }

        public static bool operator ==(Price left, Price right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Price left, Price right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            var fractional = new StringBuilder(FractionalPart.ToString());

            for (int i = fractional.Length; i <= numbersAfterDot; i++)
            {
                fractional.Append("0");
            }
            return $"{WholePart}.{FractionalPart}";
        }
    }
}
