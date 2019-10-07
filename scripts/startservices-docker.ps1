Start-Process powershell { docker run --rm -ti -p 8888:8888 -v $pwd/servers/ConfigServer/steeltoe/config-repo:/steeltoe/config-repo --name steeltoe-config steeltoeoss/servers:configserver1.0-linux }
Start-Process powershell { docker run --rm -ti -p 8761:8761 --name steeltoe-eureka steeltoeoss/eurekaserver }
Start-Process powershell { docker run --rm -ti -p 7979:7979 --name steeltoe-hystrix kennedyoliveira/hystrix-dashboard }
Start-Process powershell { docker run --rm -ti -p 8080:8080 --name steeltoe-uaa steeltoeoss/workshop-uaa-server }
Start-Process powershell { docker run --rm -ti -p 9411:9411 --name steeltoe-zipkin openzipkin/zipkin }