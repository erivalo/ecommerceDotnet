# Notes

<p>
It is worth mentioning at this point, that if we try to run migrations without RabbitMQ running, it will not work. This is due to how EF Core bootstraps the application to apply the migrations. So, if you wish to manually run migrations through the command line, ensure RabbitMQ is running.
</p>
