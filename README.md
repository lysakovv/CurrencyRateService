# CurrencyRateService
CurrencyRateService is a web service that implements API methods for getting exchange rates provided by the Central Bank of Russia. The service gets exchange rates at 11:40 AM (GMT+3).
## Web API
Currently the service has 2 API GET methods:
* /currencies - returns a list of all exchange rates. These is optionally pagination opportunity by adding ?PageNumber=(number)&PageSize=(number) to the request. Request also returns total page amount, current page and total items.
* /currencies/{id} - returns an exchange rate for currency with chosen id.
## Checking app functionality
Running project in development mode allows you to test API methods in [Swagger](https://swagger.io/).
## Request examples
### GET /currencies
```https://localhost:7258/currencies?PageNumber=2&PageSize=3```
### Response
```
{
  "currentPage": 2,
  "totalItems": 34,
  "totalPages": 12,
  "items": [
    {
      "id": "R01060",
      "numCode": "051",
      "charCode": "AMD",
      "nominal": 100,
      "name": "Армянских драмов",
      "value": 15.831,
      "previous": 15.8434
    },
    {
      "id": "R01090B",
      "numCode": "933",
      "charCode": "BYN",
      "nominal": 1,
      "name": "Белорусский рубль",
      "value": 27.2463,
      "previous": 27.3611
    },
    {
      "id": "R01100",
      "numCode": "975",
      "charCode": "BGN",
      "nominal": 1,
      "name": "Болгарский лев",
      "value": 39.1187,
      "previous": 39.7732
    }
  ]
}
```
### GET /currencies/{id}
```https://localhost:7258/currencies/R01535```
### Response
```
{
  "id": "R01535",
  "numCode": "578",
  "charCode": "NOK",
  "nominal": 10,
  "name": "Норвежских крон",
  "value": 78.201,
  "previous": 79.4224
}
```
