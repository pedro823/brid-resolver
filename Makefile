run: build
	docker run -it -p 8080:5000 -p 8443:5001 brid/resolver

build:
	docker build -t brid/resolver . 

