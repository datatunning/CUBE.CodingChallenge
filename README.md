# Temperature Converter Coding challenge [![Continuous-Integration](https://github.com/datatunning/CUBE.CodingChallenge/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/datatunning/CUBE.CodingChallenge/actions/workflows/continuous-integration.yml) [![codecov](https://codecov.io/gh/datatunning/CUBE.CodingChallenge/branch/main/graph/badge.svg?token=9VTYUHFQ0R)](https://codecov.io/gh/datatunning/CUBE.CodingChallenge)

It is important for us to get a sense of how you approach problems, your coding style, and how you produced deployable code.  

To that end we have a relative straightforward practical stage in the hiring process, which is to ask you to develop and share with us a simple application.  

## instructions

* Create a web application that allows a user to convert between Celsius, Kelvin and Fahrenheit temperature values.
* Provide access to a code repository to allow us to review the code, build and run the application (including ReadMe instructions).
* You can use any technology stack but bear in mind that we use Angular + .Net so all else being equal we recommend you do the same.

### Temperature conversion formulas

* Celsius to Fahrenheit  F = 9/5 ( C) + 32
* Kelvin to Fahrenheit  F = 9/5 (K - 273) + 32
* Fahrenheit to Celsius  C = 5/9 ( F - 32)
* Celsius to Kelvin K =  C + 273
* Kelvin to Celsius  C = K - 273
* Fahrenheit to Kelvin K = 5/9 (F - 32) + 273

## How to run
* grab the repo locally
* build and run the CUBE.CodingChallenge.API .netcore5 solution
	* The API comes with swaggerUI accessible via <http://localhost:5000 or https://localhost:5001>
* build and run the CUBE.CosingChallenge.Webapp
	* Open your browser and nagivage to <http://localhost:4200/>
