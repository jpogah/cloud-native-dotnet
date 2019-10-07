FROM java:8
ADD target/hystrix-dashboard-0.0.1.BUILD-SNAPSHOT.jar app.jar
EXPOSE 7979
ENTRYPOINT ["java","-jar","/app.jar"]