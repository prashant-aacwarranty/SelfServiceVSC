# SelfServiceVSC

Direct-To-Customer Vehicle Service Contract sales website project. The code is stored on GitHub at https://github.com/AACWarranty/SelfServiceVSC.git. I just emailed you an invite to our GitHub. The project is called SelfServiceVSC (I might have to add permissions for you to view it after you accept the invite).

We purchased a GoDaddy web url to use for this website at bestpriceprotection.com. We'll have to work together to determine if we can host this site through GoDaddy or it we need another solution.

Here is an overview of how we want the site to:

1) The customer enters their Vehicle Identification Number (VIN) and current odometer reading

2) The website uses a VIN-decoding API from the NHTSA called 'vPic' to decode the year, make and model of the vehicle and display it to the customer
            https://vpic.nhtsa.dot.gov/decoder/

3) The website sends the vin, year, make, model using an API to SCS, our SaaS contract administration system
            (We use software as a service to administrate our vehicle service contracts. The system is called SCS, owned by a company called StoneEagle. The technical contact at StoneEagle for their API is Melissa Klebosky <mklebosky@fiadmin.com>, and I will email her to introduce you as soon as you are ready. She can also send you the most-recent API documentation for their eRating and eContracting system which will allow this website to pull Vehicle Service Contract options and pricing for the customer and then submit a new contract into our system once the customer completes the purchase.)

4) SCS sends back the Service Contract options available for that customer (coverage plans, available contract terms, available deductibles, etc.) along with the pricing for each option.

5) The customer chooses which plan, term, and deductible they want to purchase, and uploads a picture of their odometer (so that we can verify the odometer reading they entered in step 1

6) The customer chooses to purchase the selected Vehicle Service Contract using either:
    A. A payment plan (through a business partner of ours called Paylink) or
    B. To pay all at once using their credit card. 

The Paylink programming is already completed, but we'd want you to review it with us and make sure it's working correctly.

 

We need to finish things up with these items:
7) Verify, step-by-step, that the existing pieces are working. 1) VIN decoding through vPic. 2) eRating through the StoneEagle/SCS API. 3) Purchase by payment plan through the Paylink API. 4) Add a shopping cart for credit card purchase to the website if the customer wants to pay all at once instead of paying by payment plan through Paylink. 5) Complete purchase by submitting the new contract into StoneEagle/SCS via their eContracting API.

8) After payment is received, the system generates a vehicle service contract PDF for the customer (through the SCS API) and sends them their completed contract PDF (both through the website and also via email)

9) The system also emails one of our staff members the completed contract, along with the odometer picture and odometer reading so that we can verify the odometer.

10) We have a hosted server, but we can run this on Amazon's AWS platform if you believe that's a better option. We'll need you to set up and test things there, then get things running once it's all ready. 
