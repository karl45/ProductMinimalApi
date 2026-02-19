First of all, you should create public and private key and write path in the config through system environment variable, because projects use two-factor authentication.


After, you should add system environment files which you can find in config

For example:
"OIDC":{
  "Server":""
}

Exists only one test user, update products doesn't work yet. 
Test user:
Login: lorem@outlook.com
Password: Ipsum34$

it means you need create variable with name OIDC__Server and etc.

this Project is server for https://github.com/karl45/market
