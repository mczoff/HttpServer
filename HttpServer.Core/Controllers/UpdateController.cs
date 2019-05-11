using HTTPServer.Core.Abstractions;
using HTTPServer.Core.Attributes;
using HTTPServer.Core.Exceptions;
using HTTPServer.Core.Model;
using HTTPServer.Core.Repositories;
using HTTPServer.Core.Repositories.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Controllers
{
    [Controller("update")]
    public class UpdateController
    {
        CountryRepository _countryRepository;
        PeopleRepository _peopleRepository;

        public UpdateController(CountryRepository countryRepository, PeopleRepository peopleRepository)
        {
            _countryRepository = countryRepository;
            _peopleRepository = peopleRepository;
        }

        [Page(@"[0-9]+")]
        public IHttpJsonResponse Post(string table, int i, [PostData]string data)
        {
            switch (table)
            {
                case "countries":
                    Country country = JsonConvert.DeserializeObject<Country>(data);
                    if (i != country.Id)
                        throw new HttpStatusCodeException(HttpStatusCode.BadRequest);

                    _countryRepository.Update(country);
                    break;
                case "peoples":
                    People people = JsonConvert.DeserializeObject<People>(data);
                    if (i != people.Id)
                        throw new HttpStatusCodeException(HttpStatusCode.BadRequest);

                    _peopleRepository.Update(people);
                    break;
                default:
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound);
            }

            return new HttpJsonResponse();
        }

        [Page(@"index")]
        public IHttpJsonResponse GetIndex()
        {
            var methodsInfo = this.GetType().GetMethods();
            return new HttpJsonResponse { Data = JsonConvert.SerializeObject(methodsInfo.Select(t => new { Name = t.Name, ReturnType = t.ReturnType, Params = t.GetParameters().Select(k => $"{k.ParameterType.Name} {k.Name}") }), Formatting.Indented) };
        }


        [Error(HttpStatusCode.NotFound, "Page with current index not found")]
        public IHttpJsonResponse NotFound()
        {
            var myAttribute = GetType().GetMethod("NotFound").GetCustomAttributes(true).OfType<ErrorAttribute>().FirstOrDefault();
            return new HttpJsonResponse { Data = $"<html><head><meta charset='utf8'></head><body>{myAttribute.Description}</body></html>" };
        }
    }
}
