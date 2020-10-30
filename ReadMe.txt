* ERROR: EACCES current user does not have permission to access the dev dir:

	sudo chown -R root "/home/ftpuser/psc/app-src/node_modules"
	sudo chown -R administrator "/home/ftpuser/psc/app-src/node_modules"

* Install NodeJS lastest from source:

	curl -sL https://deb.nodesource.com/setup_14.x | sudo -E bash -	
	sudo apt-get install -y nodejs

* Install packages for NodeJs:

	npm install formidable
	npm install form-data
	npm install zlib

	npm install ws
	npm install lodash
	npm install node-fetch
	npm install cron
	
	npm install ioredis
	npm install redis
	npm install bull

	npm install jsonwebtoken

	npm install grpc
	npm install @grpc/proto-loader
	npm install -g grpc-tools

	npm install @elastic/elasticsearch

	./protoc.exe --csharp_out=. --grpc_out=. --plugin=protoc-gen-grpc=grpc_csharp_plugin.exe message.proto


* Run application on environment Node(Linux):
﻿- node --max-old-space-size=4096 main.js


* On windows, must be install packages:
- node-v14.8.0-x64.msi
- iisnode-full-v0.2.21-x64.msi
- setup IIS website on folder "Mascot.LPA.ProxySearch"

-------------------------------------------------------------------------------
services.msc

>ping
>info replication

TASKKILL /F /IM "node*"
TASKKILL /F /IM "redis-server.exe*"

./redis-server.exe --service-install --service-name redis_5000 --port 5000 --bind 10.1.1.174
sc delete redis_5000

C:\ntest\nexe.exe "%systemdrive%\Program Files\nodejs\node.exe" "C:\ntest\app.js" --service-install --service-name node-6369

sc.exe create node-6369 binPath= "C:\ntest\nexe.exe"

sc delete node-6369

