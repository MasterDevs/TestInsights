# TestInsights [![Build status](https://ci.appveyor.com/api/projects/status/5e9mu7n11q7oy2hq/branch/master?svg=true)](https://ci.appveyor.com/project/jquintus/testinsights/branch/master) [![NuGet version](https://badge.fury.io/nu/MasterDevs.TestInsights.svg)](https://www.nuget.org/packages/MasterDevs.TestInsights/)

Adds an attribute to easily track performance tests using NUnit and Application Insights

## Prerequisites:  

### NUnit
TestInsights is build on top of NUnit.  With it, you can add instrumentation to any of your existing unit tests.

### Application Insights configured on Azure 
In order to use TestInsights, you need to have an Application insights endpoint configured in Azure.  

* [Getting Started with Application Insights](https://azure.microsoft.com/en-us/documentation/articles/app-insights-get-started/)
* [Start Monitoring with Application Insights](https://azure.microsoft.com/en-us/documentation/articles/app-insights-start-monitoring-app-health-usage/)

## Quick Start

### 1.  Get From NuGet

    Install-Package MasterDevs.TestInsights

### 2. Add the PerfSetup attribute with your InstrumentationKey
You will need the instrumentation key that corresponds to the Application Insights configured in the prerequisites.

To find your InstrumentationKey log on to your [Azure instanc](portal.azure.com) and 

![](readmeImages\instrumentationKey.png)

This only needs to be added once.

    using MasterDevs.TestInsights;
    // ...
    [assembly: PerfSetup("3d9e18a2-8c48-8c48-8c48-cb0bb325679")]

### 3. Add the Perf attribute to any test you want to instrument

```csharp
[Test]
[Perf]
public void ShortTest()
{
    Assert.IsTrue(true);
}
```

## Thanks

* Thanks to [NUnit](http://nunit.org)
