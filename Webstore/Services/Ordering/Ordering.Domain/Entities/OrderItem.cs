﻿using Ordering.Domain.Common;
using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public string ProductName { get; private set; } 
        public string ProductId { get; private set; }
        public string PictureUrl { get; private set; }
        public decimal Price { get; private set; }
        public int Units { get; private set; } = 0;

        public OrderItem(string productName, string productId, string pictureUrl, decimal price, int units)
        {
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
            PictureUrl = pictureUrl ?? throw new ArgumentNullException(nameof(pictureUrl));
            Price = price;
            AddUnits(units);
        }

        public void AddUnits(int units) {
            var newUnits = Units + units;
            if (newUnits <= 0)
            {
                throw new OrderingDomainException("Invalid number of new units for order item");
            }
            Units = newUnits;
        }
    }
}

// entiteti bi trebalo da imaju neku logiku koja je implementirana samo za njih
// npr ja ne mogu da menjam  broj Units zbog private set(da ne bi stavio da ima -5 unita)
// logika mog domena kaze da ne mogu da imam negativne ili 0 units
// zato bi trebalo da napravimo metod AddUnits(int units)
