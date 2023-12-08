# App details

- Code refactored according to the task (see below).
- Registration works for GB, FR or DE.
- Providing other than that, or leaving the field blank, will give You the 400 Bad Request,
  with message "Country not supported".
- From the task perspective the CompanyName and Id do not matter, 
  therefore it is not checked, You can leave those fields even blank if You want,
  the app will still work.
- The Registration methods in the 3 service classes are covered with one-one unit tests, all succeeded.

- To run the app, just pull it from the repo, and it is ready to go.




# Taxually technical test

This solution contains an [API endpoint](https://github.com/Taxually/developer-test/blob/main/Taxually.TechnicalTest/Taxually.TechnicalTest/Controllers/VatRegistrationController.cs) to register a company for a VAT number. Different approaches are required based on the country where the company is based:

- UK companies can register via an API
- French companies must upload a CSV file
- German companies must upload an XML document

We'd like you to refactor the existing solution with the following in mind:

- Readability
- Testability
- Adherance to SOLID principles

We'd also like you to add some tests to show us how you'd test your solution, although we aren't expecting exhaustive test coverage.

We don't expect you to implement the classes for making HTTP calls and putting messages on queues.

We'd like you to spend not more than a few hours on the exercise.

To develop and submit your solution please follow these steps:

1. Create a public repo in your own GitHub account and push the technical test there
2. Develop your solution and push your changes to your own public GitHub repo
3. Once you're happy with your solution send us a link to your repo