# Hangfire.Extensions.Configuration

Functionality to read [Hangfire](https://www.hangfire.io/) configurations from key-value pair based configuration.

## Install

```PowerShell
PM> Install-Package Hangfire.Extensions.Configuration -Version 1.0.0
```

## Functionality

* GetHangfireDashboardOptions: Gets the [Hangfire](https://www.hangfire.io/) dashboard options.
* GetHangfireBackgroundJobServerOptions: Gets the [Hangfire](https://www.hangfire.io/) background job server options.

## Usage

1. Add this NuGet package to your project.
2. In your ```appsettings.json``` add the following section:
	```json
	"Hangfire": {
		"Dashboard": {
			"AppPath": "/",
			"StatsPollingInterval": 2000
		},
		"Server": {
			"HeartbeatInterval": "00:00:30",
			"Queues": [ "default" ],
			"SchedulePollingInterval": "00:00:15",
			"ServerCheckInterval": "00:05:00",
			"ServerName": null,
			"ServerTimeout": "00:05:00",
			"ShutdownTimeout": "00:00:15",
			"WorkerCount": 20
		}
	}
	```
3. In your ```startup.cs``` add the following using statement:
	```C#
	using Hangfire.Extensions.Configuration;
	```
4. In your ```startup.cs``` add the following lines in the ```configure``` method:
	```C#
	app.UseHangfireDashboard("/hangfire", Configuration.GetHangfireDashboardOptions());
	app.UseHangfireServer(Configuration.GetHangfireBackgroundJobServerOptions());
	```

## Change Log

### Version 1.0.0

#### New Features

* Added support to get [Hangfire](https://www.hangfire.io/) dashboard options from [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration "IConfiguration Interface") (GetHangfireDashboardOptions)
* Added support to get [Hangfire](https://www.hangfire.io/) background job server options from [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration "IConfiguration Interface")(GetHangfireBackgroundJobServerOptions)

## References

* [Hangfire](https://www.hangfire.io/)
