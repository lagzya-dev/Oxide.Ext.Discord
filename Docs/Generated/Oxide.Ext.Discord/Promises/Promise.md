# Promise class

Implements a non-generic C# promise, this is a promise that simply resolves without delivering a value. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise

```csharp
public sealed class Promise : BasePromise, IPendingPromise
```

## Public Members

| name | description |
| --- | --- |
| [Promise](#Promise-constructor)() | Constructor for the promise |
| static [Create](#Create-method)() |  |
| [Catch](#Catch-method)(…) |  |
| [Catch&lt;TException&gt;](#Catch-method)(…) |  |
| [ContinueWith](#ContinueWith-method)(…) |  |
| [ContinueWith&lt;TConvert&gt;](#ContinueWith-method)(…) |  |
| [Finally](#Finally-method)(…) |  |
| [Resolve](#Resolve-method)() |  |
| [Then](#Then-method)(…) |  (5 methods) |
| [Then&lt;TConvert&gt;](#Then-method)(…) |  (2 methods) |
| [ThenAll](#ThenAll-method)(…) |  |
| [ThenAll&lt;TConvert&gt;](#ThenAll-method)(…) |  |
| [ThenSequence](#ThenSequence-method)(…) |  |
| static [All](#All-method)(…) | Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results. (2 methods) |
| static [Rejected](#Rejected-method)(…) | Convert an exception directly into a rejected promise. |
| static [Resolved](#Resolved-method)() | Returns a promise that has been resolved |
| static [Sequence](#Sequence-method)(…) | Chain a number of operations using promises. Takes a number of functions each of which starts an async operation and yields a promise. (2 methods) |

## Protected Members

| name | description |
| --- | --- |
| override [ClearHandlers](#ClearHandlers-method)() | Helper function clear out all handlers after resolution or rejection. |
| override [EnterPool](#EnterPool-method)() |  |

## See Also

* class [BasePromise](./BasePromise.md)
* interface [IPendingPromise](../Interfaces/Promises/IPendingPromise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [Promise.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Promises/Promise.cs)
   
   
# Create method

```csharp
public static Promise Create()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Resolved method

Returns a promise that has been resolved

```csharp
public static IPromise Resolved()
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Rejected method

Convert an exception directly into a rejected promise.

```csharp
public static IPromise Rejected(Exception ex)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ClearHandlers method

Helper function clear out all handlers after resolution or rejection.

```csharp
protected override void ClearHandlers()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Resolve method

```csharp
public void Resolve()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Catch method (1 of 2)

```csharp
public IPromise Catch(Action<Exception> onRejected)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Then method (1 of 7)

```csharp
public IPromise Then(Action onResolved)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# ThenAll method (1 of 2)

```csharp
public IPromise ThenAll(Func<IEnumerable<IPromise>> chain)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# All method (1 of 2)

Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results.

```csharp
public static IPromise All(IEnumerable<IPromise> promisesEnumerable)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# ThenSequence method

```csharp
public IPromise ThenSequence(Func<IEnumerable<Func<IPromise>>> chain)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Sequence method (1 of 2)

Chain a number of operations using promises. Takes a number of functions each of which starts an async operation and yields a promise.

```csharp
public static IPromise Sequence(params Func<IPromise>[] fns)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Finally method

```csharp
public IPromise Finally(Action onComplete)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ContinueWith method (1 of 2)

```csharp
public IPromise ContinueWith(Func<IPromise> onComplete)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# EnterPool method

```csharp
protected override void EnterPool()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Promise constructor

Constructor for the promise

```csharp
public Promise()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
