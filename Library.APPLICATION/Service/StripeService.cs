using Library.APPLICATION.DTO.Checkout;
using Library.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Service
{
    public class StripeService
    {
        private readonly IBookInterface _book;

        public StripeService(IBookInterface book)
        {
            _book = book;
        }
        public async Task<GetCheckoutDTOs> CreateCheckoutSession(TransactionCheckoutDTOs checkoutDTOs, HttpRequest Request)
        {
            var books = new List<DOMAIN.MODEL.Books>();
            decimal totalAmount = 0;
            foreach (var bookId in checkoutDTOs.BookId)
            {
                var book = await _book.GetById(bookId);
                if (book is null)
                {
                    return new GetCheckoutDTOs
                    {
                        IsCheckedOut = false,
                        Message = "Some books not found",
                        UrlStribe = string.Empty
                    };
                }
                    books.Add(book);
                    totalAmount += book.Price;
            }
            if (checkoutDTOs.PaymentMethod.ToLower() == "visa")
            {

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {

                    },

                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
                };
                foreach (var book in books)
                {

                    options.LineItems.Add(new SessionLineItemOptions
                    {


                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = book.Title,
                                Description = book.Description,
                            },
                            UnitAmount = (long)book.Price,
                        },
                        Quantity = checkoutDTOs.BookId.Count(),
                    });
                }


                var service = new SessionService();
                var session = service.Create(options);


                return new GetCheckoutDTOs
                {
                    IsCheckedOut = true,
                    Message = "Checkout session created successfully",
                    UrlStribe = session.Url
                };

            }
            else if (checkoutDTOs.PaymentMethod.ToLower() == "cash")
            {
                return new GetCheckoutDTOs
                {
                    IsCheckedOut = true,
                    Message = "Done payment method",
                    UrlStribe = string.Empty
                };
            }
            else
            {
                return new GetCheckoutDTOs
                {
                    IsCheckedOut = false,
                    Message = "Invalid payment method",
                    UrlStribe = string.Empty
                };

            }
        }
    }
}
