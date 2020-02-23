# README

This is a .NET CORE API project which aims to manage users and accounts. Testing project is also included. 

## Note
Since this project uses localDB, please run it on Windows. 

## Test on Postman
## Get list (Get)
user list : .../api/user
account list : .../api/account

## Create a new user (Post)
url : .../api/user/CreatUser

header : Content-Type : application/json

body : 
{
	"name" : "test",
  "email" : "test@test.com",
  "salary" : 10000,
  "expense": :5000
}


## Get a user by user email (Get)
url : .../api/user/GetUserByEmail/test@test.com

## Get a user by user id (Get)
url : .../api/user/GetUserById/1

## Create a new account (Post)
url : .../api/account/CreateAccountByUserId
header : Content-Type : application/json

body : 
{
	"id":1
}

