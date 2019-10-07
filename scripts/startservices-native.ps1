Start-Process powershell { Set-Location ./servers/ConfigServer;./mvnw spring-boot:run }
Start-Process powershell { Set-Location ./servers/EurekaServer;./mvnw spring-boot:run }
Start-Process powershell { Set-Location ./servers/HystrixDashboard;./mvnw spring-boot:run }
Start-Process powershell { Set-Location ./servers/UAA;./gradlew cargoRunLocal -x javadoc -x javadocJar }
Start-Process powershell { Set-Location ./servers/OpenZipkin;java -jar zipkin.jar }