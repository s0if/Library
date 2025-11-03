using Library.APPLICATION.DTO.Checkout;
using Library.APPLICATION.DTO.Publisher;
using Library.APPLICATION.Mapping;
using Library.APPLICATION.Service;
using Library.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Publisher
{
    public class AddPublisherUseCase
    {
        private readonly IPublisherInterface _publisher;
        private readonly PublisherMap _publisherMap;
        private readonly StripeService _stripe;
        private readonly IBookPublisherInterface _bookPublisher;

        public AddPublisherUseCase(IPublisherInterface publisher,PublisherMap publisherMap, StripeService stripe,IBookPublisherInterface bookPublisher ) 
        {
            _publisher = publisher;
            _publisherMap = publisherMap;
            _stripe = stripe;
            _bookPublisher = bookPublisher;
        }
        public async Task<GetCheckoutDTOs> Execute (TransactionCheckoutDTOs publisherDTOs,string UserId,HttpRequest request)
        {
            var result = await _publisherMap.ToPublisher(publisherDTOs, UserId);
            if (!result.publishers.Any()||!result.bookPublishers.Any())
                throw new Exception();
            var stripeCustomer = await _stripe.CreateCheckoutSession(publisherDTOs, request);
          
            if(stripeCustomer.IsCheckedOut==false)
                throw new Exception("Stripe checkout session failed.");
            foreach (var i in result.publishers)
            {
                await _publisher.Add(i);
            }
            foreach (var i in result.bookPublishers)
            {
                await _bookPublisher.Add(i);
            }
            return stripeCustomer;

        }
    }
}
