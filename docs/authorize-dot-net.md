
# about

https://developer.authorize.net/


# example usage in C#

https://github.com/AuthorizeNet/sample-code-csharp/blob/master/PaymentTransactions/ChargeCreditCard.cs

# testing

Authorize.net provides test card numbers for various testing scenarios outlined in the testing guide: https://developer.authorize.net/hello_world/testing_guide.html


# legal stuff

I think this app is required to comply with PCI-DSS, maybe talk to a lawyer.

https://www.authorize.net/resources/blog/2021/pci-dss-compliance.html

I thiiiink the type C self-assessment is what you need to fill out to know what you need to do to comply fully.

SAQ C is for any merchant with a payment application connected to the Internet, but there is no electronic cardholder data storage.


## test credentials

These were generated for the sandbox.

api-login: 6uZeB99yHx
tran-key: 39feLFw3745s4Np2
key: SIMON

## American Express
370000000000002

## Discover
6011000000000012

## JCB
3088000000000017

## Diners Club / Carte Blanche
38000000000006

## Visa
4007000000027
4012888818888
4111111111111111

## Mastercard
5424000000000015
2223000010309703
2223000010309711


## test endpoint

In DEBUG configuration, exposes a `/test/pay` endpoint which runs a transaction through without requiring all the other setup.


```

$json_data = '{
  "cardNumber": "4111111111111111",
  "expiration": "1123",
  "cardCode": "123",
  "firstName": "Bob",
  "lastName": "Smith",
  "address": "123 Fake Street",
  "city": "Fakeville",
  "zip": "77777",
}'

$json_data | curl --insecure -X POST -d - https://localhost:7238/test/pay

```

Example response:

```json
{
  "transactionResponse": {
    "responseCode": "1",
    "authCode": "A80PZI",
    "avsResultCode": "Y",
    "cvvResultCode": "P",
    "cavvResultCode": "2",
    "transId": "60223421156",
    "refTransID": "",
    "transHash": "",
    "testRequest": "0",
    "accountNumber": "XXXX1111",
    "accountType": "Visa",
    "messages": [
      {
        "code": "1",
        "description": "This transaction has been approved."
      }
    ],
    "transHashSha2": "",
    "SupplementalDataQualificationIndicator": 0,
    "networkTransId": "0W5ILTWHKI6QWCZ8HNSJMLC"
  },
  "messages": {
    "resultCode": "Ok",
    "message": [
      {
        "code": "I00001",
        "text": "Successful."
      }
    ]
  }
}

```