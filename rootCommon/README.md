# Notes

## RSA public / private key generation
1. openssl genrsa -des3 -out private.pem 2048
2. openssl rsa -in private.pem -outform PEM -pubout -out public.pem
