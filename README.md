# Sample ASP.NET Core Web API to Send and Verify 2FA Code
Based On

.Net Core 6.0

Entity Framework Core

SQL Server

## Swagger Documentation


``` http://localhost:5000/swagger ```

![image](https://github.com/anilbabaria23/Arival/assets/66458751/650b1d48-ab44-4a7b-b3d7-4e2e14cc96b3)

## Generate 2FA Code

This is a POST method which will accept phone number In order to generate 2FA code.

### Sample cURL

```
curl --location 'http://localhost:5000/api/AuthCode/generate2FACode' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
  "mobileNumber": "9979388070"
}'

```

![image](https://github.com/anilbabaria23/Arival/assets/66458751/e40313fb-1eb0-4f41-ba39-e16ff0b116cd)

## Verify 2FA Code

This is a POST method which will accept phone number and 2FA Code recieved In order to verify whether the 2FA code sent is valid or not.

### Sample cURL

```
curl --location 'http://localhost:5000/api/AuthCode/verify2FACode' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
  "mobileNumber": "9979388070",
  "verificationCode": "406431"
}'

```

![image](https://github.com/anilbabaria23/Arival/assets/66458751/32168b40-3775-4295-bf3e-fd75f50cc80f)
