# Contoso 🍕 Pizza Ordering - ASP.Net Web Api and Web Application with SignalR - Demo and Tutorial

Table of Contents

##  1. <a name='ProjectOverview'></a>Project Overview

A simple 🍕 pizza ordering web application (including a web API) with SignalR updates for customers and the admin pizza shop. A demo video is available that showcases the project in action and discusses the technical implementation.

Customers can order pizza and receive real-time updates on their orders using SignalR.
Pizza shop owners can receive real-time orders from customers and make order status updates that are delivered as real-time updates to their customers via SignalR.

##	2. <a name='DemoLinks'></a>Demo Links
Depending on the availability of the free hosting service, these links may be unavailable, but let's stay positive 😁

[🧍 Customer Pizza Order Page](http://orderpizzademo.runasp.net/)

![Customer Pizza Order Page](images/customerpage.jpg)

[👩‍🍳 Pizza Shop Admin Page](http://orderpizzademo.runasp.net/admin.html)

![Pizza Shop Admin Page](images/adminpage.jpg)

[🌐 Demo Swagger Link](http://orderpizzademo.runasp.net/swagger/index.html)

![Demo Swagger Page](images/swaggerpicture.jpg)


##  3. <a name='Architecture'></a>Architecture
The visual studio solution is an asp.net web api with controllers, models, and services. There is one singleton service for the Orders management. Setup for SignalR is included with the OrdersHub and callback registrations. There are also static web pages for both the customer and admin pizza ordering interface to the web api and real-time updates connectivity using SignalR.

##  4. <a name='WebApiCRUD'></a>Web Api CRUD
This is a simple web api that follows the standard CRUD (create, read, update, and delete) pattern. Demo images of the api operations are shown below by category.


[Swagger Documentation](http://orderpizzademo.runasp.net/swagger/v1/swagger.json)
[Demo Swagger Link](http://orderpizzademo.runasp.net/swagger/index.html)



