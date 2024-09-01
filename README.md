<h1 align="center">B2BMart</h1>

<h3>Problem Statement</h3>

<strong>Demand Forecasting</strong><p>Develop a model to identify and analyze factors influencing demand in the logistics industry. Participants should propose methods to predict fluctuations in demand accurately.</p>

<p align="center">
  <img src="docs/B2B.png" />
</p>

<h2>Step 1</h2>
<ul>
<li>Create a B2B E-commerce site similar to Alibaba or Amazon, with the following features:</li>
<ul>
<li> It will have a feature to register a user based on email, and phone number (via OTP service). Users will be of two types: Seller (who will list/delete the products) and Customer (who can scroll through the catalog and place orders).
    </li><li> User authentication and authorization.
    </li><li> Users will be able to search and sort the product based on the keyword.
    </li><li> Customers will have the option to review or add products to the cart or buy directly.
    </li><li> Users will receive notifications by email and SMS for every activity they do (place an order, cancel an order, etc). 
    </li><li> Users can search through the placed order (order history).
    </li><li> There will be a service that will generate an invoice and share it via Email. Users will have an option too to download it.
</li></ul>
<li>We are creating a Java Spring Boot Application to take care of the Backend API.</li>
<li>We are using Kafka for message-broker.</li>
<li>The Client Side has been written in Angular 17.1.0</li>
<li>We will be creating a product recommendation system using Python, TensorFlow, and ML Ops to deploy a Recommendation System Model in AWS.</li>
</ul></div>

<h2>Step 2</h2>

<li>To develop a demand forecasting model for the logistics industry, we need to identify the key factors influencing demand and choose appropriate methods for accurately predicting demand fluctuations. Here's a structured approach to help with this task:</li>
    </li><li> User authentication and authorization.
    </li><li> Users will be able to search and sort the product based on the keyword.
    </li><li> Customers will have the option to review or add products to the cart or buy directly.
    </li><li> Users will receive notifications by email and SMS for every activity they do (place an order, cancel an order, etc). 
    </li><li> Users can search through the placed order (order history).
    </li><li> There will be a service that will generate an invoice and share it via Email. Users will have an option too to download it.


<li>Create a B2B E-commerce site similar to Alibaba or Amazon, with the following features:</li>

<h3>Application Demo</h3>

For video demostration refer to the YouTube link <a href="">here.</a> 

<h3>Project Architecture</h3>


<p align="center">
  <img src="docs/B2BMart.png" />
</p>

<p align="center">
  <img src="docs/B2B_Architecture.png" width="900" height="650" />
</p>


<h3>DataBase Architecture</h3>

<p align="center">
  <img src="docs/B2B_DataBase.png" width="900" height="650" />
</p>





<h3>Application Screenshots</h3>

<br />
<p align="center">
  <img src="docs/1.png" width="400"/>
  <img src="docs/2.png" width="400"/>
  <img src="docs/3.png" width="400"/>
  <img src="docs/4.png" width="400"/>
  <img src="docs/5.png" width="400"/>
  <img src="docs/6.png" width="400"/>
  <img src="docs/7.png" width="400"/>
  <img src="docs/8.png" width="400"/>
  <img src="docs/9.png" width="400"/>
  <img src="docs/10.png" width="400"/>
  <img src="docs/11.png" width="400"/>
  <img src="docs/12.png" width="400"/>
  <img src="docs/13.png" width="400"/>
  <img src="docs/14.png" width="400"/>
</p>





### Clone/Download the Repository

```
git clone https://github.com/IntelegixLabs/B2BMart
```

## Scaffolding the Tables

```
PM> Scaffold-DbContext -Connection "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=B2BMart;Trusted_Connection=true;Integrated Security=true" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -OutputDir Models -force -Verbose
```



## Running the Dot Net Application

```
cd B2BMart\src\backend\B2BMart.All
dotnet clean
dotnet build B2BMart.API.sln
cd B2BMart.API
dotnet watch run --B2BMart.API
```


## Running Docker and setting up Redis server and <a href="https://localhost:8081"> Redis Commander</a>

```
cd B2BMart
docker-compose up --detach
Open Redis Commander at - localhost:8081
```
## Running the Angular Client in Local

```
cd B2BMart\src\client\B2BMartApp
npm install
ng serve
```

## Running the stripe Webhook

```
Install the Stripe CLI Tools
stripe login  
stripe listen
stripe listen -f https://localhost:64728/api/Payments/webhook
stripe listen -f https://localhost:64728/api/Payments/webhook -e payment_intent.succeeded,payment_intent.failed
```

## Running the Angular Client 

```
https://main.d20n0xnzfjgghx.amplifyapp.com/account/login
```
