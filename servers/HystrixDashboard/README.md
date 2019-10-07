# Hystrix Dashboard

To run locally:

```bash
./mvnw spring-boot:run
```

In your browser, go to [http://localhost:7979/](http://localhost:7979/) # port configurable in `application.yml`

On the home page is a form, where you can enter the URL for an event stream to monitor.
For example: `http://localhost:9000/hystrix.stream`.

To aggregate many streams together you can use the
[Turbine sample](https://github.com/spring-cloud-samples/turbine).
