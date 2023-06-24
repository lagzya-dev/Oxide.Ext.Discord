# Promise&lt;TPromised&gt; class

Implements a C# promise. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise

```csharp
public sealed class Promise<TPromised> : BasePromise, IPendingPromise<TPromised>
```

## Public Members

| name | description |
| --- | --- |
| [Promise](#Promise-constructor)() | Constructor |
| static [Create](#Create-method)() | Returns a promise that is currently pending |
| [Catch](#Catch-method)(…) |  (2 methods) |
| [Catch&lt;TException&gt;](#Catch-method)(…) |  |
| [ContinueWith](#ContinueWith-method)(…) |  |
| [ContinueWith&lt;TConvert&gt;](#ContinueWith-method)(…) |  |
| [Finally](#Finally-method)(…) |  |
| [Resolve](#Resolve-method)(…) |  |
| [Then](#Then-method)(…) |  (4 methods) |
| [Then&lt;TConvert&gt;](#Then-method)(…) |  (3 methods) |
| [ThenAll](#ThenAll-method)(…) |  |
| [ThenAll&lt;TConvert&gt;](#ThenAll-method)(…) |  |
| static [All](#All-method)(…) | Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results. (2 methods) |
| static [Create](#Create-method)(…) |  |
| static [Create&lt;TConvert&gt;](#Create-method)() | Returns a promise that is currently pending |
| static [Rejected](#Rejected-method)(…) | Convert an exception directly into a rejected promise. |
| static [Resolved](#Resolved-method)(…) | Convert a simple value directly into a resolved promise. |

## Protected Members

| name | description |
| --- | --- |
| override [ClearHandlers](#ClearHandlers-method)() |  |
| override [EnterPool](#EnterPool-method)() |  |

## See Also

* class [BasePromise](./BasePromise.md)
* interface [IPendingPromise&lt;TPromised&gt;](../Interfaces/Promises/IPendingPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [Promise.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Promises/Promise.cs)
   
   
# Create method (1 of 3)

Returns a promise that is currently pending

```csharp
public static Promise Create()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Resolved method

Convert a simple value directly into a resolved promise.

```csharp
public static IPromise<TPromised> Resolved(TPromised promisedValue)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Rejected method

Convert an exception directly into a rejected promise.

```csharp
public static IPromise<TPromised> Rejected(Exception ex)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Resolve method

```csharp
public void Resolve(TPromised value)
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Catch method (1 of 3)

```csharp
public IPromise Catch(Action<Exception> onRejected)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Then method (1 of 7)

```csharp
public IPromise<TPromised> Then(Action<TPromised> onResolved)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# ThenAll method (1 of 2)

```csharp
public IPromise ThenAll(Func<TPromised, IEnumerable<IPromise>> chain)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# All method (1 of 2)

Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results.

```csharp
public static IPromise<IEnumerable<TPromised>> All(
    IEnumerable<IPromise<TPromised>> promisesEnumerable)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# Finally method

```csharp
public IPromise<TPromised> Finally(Action onComplete)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/Promises/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ContinueWith method (1 of 2)

```csharp
public IPromise ContinueWith(Func<IPromise> onComplete)
```

## See Also

* interface [IPromise](../Interfaces/Promises/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---
   
   
# ClearHandlers method

```csharp
protected override void ClearHandlers()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# EnterPool method

```csharp
protected override void EnterPool()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Promise&lt;TPromised&gt; constructor

Constructor

```csharp
public Promise()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
