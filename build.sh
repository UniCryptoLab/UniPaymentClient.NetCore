#!/bin/bash

supervisorctl stop srv.demo

git pull
dotnet publish UniPayment.Client.Example -c release --no-cache --output /opt/srv.demo/release

if [ -f "/opt/srv.demo/appsettings.Production.json" ]; then
	cp -f /opt/srv.demo/appsettings.Production.json /opt/srv.demo/release
fi


supervisorctl start srv.demo
