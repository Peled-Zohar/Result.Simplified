<div align="center">

# Result.Simplified
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)  
[![Build Status](https://github.com/Peled-Zohar/Result.Simplified/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Peled-Zohar/Result.Simplified/actions/workflows/dotnet.yml/badge.svg) 
[![codecov](https://codecov.io/gh/Peled-Zohar/Result.Simplified/graph/badge.svg?token=BLASRCXG68)](https://codecov.io/gh/Peled-Zohar/Result.Simplified)  
[![NuGet version](https://img.shields.io/nuget/v/Result.Simplified.svg)](https://www.nuget.org/packages/Result.Simplified) 
[![NuGet downloads](https://img.shields.io/nuget/dt/Result.Simplified.svg)](https://www.nuget.org/packages/Result.Simplified)  
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blueviolet.svg)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![.NET Standard 2.0](https://img.shields.io/badge/.NETStandard-2.0-blueviolet.svg)](https://learn.microsoft.com/en-us/dotnet/standard/net-standard)

</div>

**Result.Simplified** enables dot net methods to return an indication of success or failure, for any method return type (including void).
It shouldn't be used instead of exceptions, but rather to enable a method to return a failure indication in non-exceptional circumstances.

### Note:
The `Result` class colided with the `Result.Simplified` namespace, so I've added a `VoidResult` class to replace it.
In the current version (2.2.0), both `Result` and `VoidResult` exists, but `Result` is marked as `Obsolete`. 
You can still use the `Result` class in 2.2.0, but in 3.0.0 it will removed completely.
---

Use `VoidResult` to enable void methods to return an indication of success or failure, 
and `Result<T>` to enable non-void methods to do the same.

Both `VoidResult` and `Result<T>` overload the `&` and `|` operators as well as the `true` and `false` operators, 
meaning you can easily combine results in a short-circuit manner for easy validations.  


### Usage example:

**Return a *`VoidResult`* from a method:**
```csharp
VoidResult DoSomething()
{
    // some code...
    return condition
        ? VoidResult.Success()
        : VoidResult.Fail("Something went wrong...");
}
```
or
```csharp
VoidResult DoSomething()
{
    // some code...
    return VoidResult.SuccessIf(condition, "Something went wrong...");
}
```
or
```csharp
VoidResult DoSomething()
{
    // some code...
    return VoidResult.FailIf(negativeCondition, "Something went wrong...");
}
```


**Return a *`Result<T>`* from a method:**
```csharp
Result<int> DoSomethingAndReturnAnInt()
{
    // some code...
    return condition
        ? Result<int>.Success(5)
        : Result<int>.Fail("Something went wrong...");
}
```
or 
```csharp
Result<int> DoSomethingAndReturnAnInt()
{
    // some code...
    return Result<int>.SuccessIf(condition, 5, "Something went wrong...", false);
    // The boolean at the end determines whether to include the value in the failed result.
}
```
or
```csharp
Result<int> DoSomethingAndReturnAnInt()
{
    // some code...
    return Result<int>.FailIf(negativeCondition, 5, "Something went wrong...", true);
    // The boolean at the end determines whether to include the value in the failed result.
}
```

**Consume a method that returns a *`VoidResult`***
```csharp
void DoIfMethodSucceeded()
{
    var result = DoSomething();
    if(!result.IsSuccess)
    {   
        // Something went wrong, do something with result.ErrorDescription 
        // log or show the user or whatever
        return;
    }
    // Everything is fine, you can go on with your code
}
```

**Consume a method that returns a *`Result<T>`***
```csharp
bool DoIfMethodSucceeded()
{
    var result = DoSomethingAndReturnAnInt();
    if(!result.IsSuccess)
    {   
        // Something went wrong, do something with result.ErrorDescription 
        // Log or show the user or whatever
        return false;
    }
    var intValue = result.Value;
    // Everything is fine, you can go on with your code
}
```

**Note:** Since `VoidResult` and `Result<T>` overloads the `true` and `false` operators,
you don't technically have to use the `IsSuccess` property to check if the result is a success or not,
but I do recommend it for readability.

```csharp
void DoIfMethodSucceeded()
{
    var result = DoSomething();
    if(!result)
    {   
        // Something went wrong, do something with result.ErrorDescription 
        // log or show the user or whatever
        return;
    }
    // Everything is fine, you can go on with your code
}
```

Since `VoidResult` and `Result<T>` overloads the `&` and `|` operators as well,
you can combine multiple results in a short-circuit manner.

```csharp
Result FailFast()
{
    var result = DoSomething() // returns a result instance
        && DoSomethingElse() // returns another result instance
        && DoAnotherThing() // returns another result instance;

    // result is the first failed result, or the last one if all succeeded.

    return result;
}

Result SuccessIfAny()
{

    var result = DoSomething() // returns a result instance
        || DoSomethingElse() // returns another result instance
        || DoAnotherThing() // returns another result instance;
    
    // result is the first successful result, or the last one if all failed.

    return result;    
}
```

**You can chain result objects for validation:**
```csharp
Result<SomeObject> Validate(SomeObject someObject)
{
    
    return Validate("someObject is null.", d => d is object) 
        && Validate("someObject has no SomeProperty.", d => d.SomeProperty is object) 
        && Validate("SomeProperty is invalid.", d => d.SomeProperty.IsValid) 
        && Validate("SomeCollection is empty.", d => (d.SomeCollection?.Count ?? 0) > 0);

    Result<SomeObject> Validate(string errorMessage, Predicate<SomeObject> predicate)
    {
        var isValid = predicate(someObject);
        if (!isValid)
        {
            // Optionally log non-exceptional error here...
        }
        return isValid 
            ? Result<SomeObject>.Success(someObject) 
            : Result<SomeObject>.Fail(errorMessage);
    }
}
```
