# IPromise interface

Implements a non-generic C# promise, this is a promise that simply resolves without delivering a value. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise

```csharp
public interface IPromise
```

## Members

| name | description |
| --- | --- |
| [Id](#Id) { get; } | ID of the promise, useful for debugging. |
| [Catch](#Catch)(…) | Handle errors for the promise. |
| [Catch&lt;TException&gt;](#Catch)(…) | Catches a specified exception |
| [ContinueWith](#ContinueWith)(…) | Add a callback that chains a non-value promise. ContinueWith callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The state of the returning promise will be based on the new non-value promise, not the preceding (rejected or resolved) promise. |
| [ContinueWith&lt;TConvert&gt;](#ContinueWith)(…) | Add a callback that chains a value promise (optionally converting to a different value type). ContinueWith callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The state of the returning promise will be based on the new value promise, not the preceding (rejected or resolved) promise. |
| [Finally](#Finally)(…) | Add a finally callback. Finally callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The returned promise will be resolved or rejected, as per the preceding promise. |
| [Then](#Then)(…) | Add a resolved callback that chains a non-value promise. (5 methods) |
| [Then&lt;TConvert&gt;](#Then)(…) | Add a resolved callback that chains a value promise (optionally converting to a different value type). (2 methods) |
| [ThenAll](#ThenAll)(…) | Chain an enumerable of promises, all of which must resolve. The resulting promise is resolved when all of the promises have resolved. It is rejected as soon as any of the promises have been rejected. |
| [ThenAll&lt;TConvert&gt;](#ThenAll)(…) | Chain an enumerable of promises, all of which must resolve. Converts to a non-value promise. The resulting promise is resolved when all of the promises have resolved. It is rejected as soon as any of the promises have been rejected. |
| [ThenSequence](#ThenSequence)(…) | Chain a sequence of operations using promises. Return a collection of functions each of which starts an async operation and yields a promise. Each function will be called and each promise resolved in turn. The resulting promise is resolved after each promise is resolved in sequence. |

## See Also

* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [IPromise.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Interfaces/Promises/IPromise.cs)
   
   
# Catch method (1 of 2)

Handle errors for the promise.

```csharp
public IPromise Catch(Action<Exception> onRejected)
```

## See Also

* interface [IPromise](./IPromise.md)
* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---
   
   
# Then method (1 of 7)

Add a resolved callback.

```csharp
public IPromise Then(Action onResolved)
```

## See Also

* interface [IPromise](./IPromise.md)
* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---
   
   
# ThenAll method (1 of 2)

Chain an enumerable of promises, all of which must resolve. The resulting promise is resolved when all of the promises have resolved. It is rejected as soon as any of the promises have been rejected.

```csharp
public IPromise ThenAll(Func<IEnumerable<IPromise>> chain)
```

## See Also

* interface [IPromise](./IPromise.md)
* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---
   
   
# ThenSequence method

Chain a sequence of operations using promises. Return a collection of functions each of which starts an async operation and yields a promise. Each function will be called and each promise resolved in turn. The resulting promise is resolved after each promise is resolved in sequence.

```csharp
public IPromise ThenSequence(Func<IEnumerable<Func<IPromise>>> chain)
```

## See Also

* interface [IPromise](./IPromise.md)
* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# Finally method

Add a finally callback. Finally callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The returned promise will be resolved or rejected, as per the preceding promise.

```csharp
public IPromise Finally(Action onComplete)
```

## See Also

* interface [IPromise](./IPromise.md)
* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
   
   
# ContinueWith method (1 of 2)

Add a callback that chains a non-value promise. ContinueWith callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The state of the returning promise will be based on the new non-value promise, not the preceding (rejected or resolved) promise.

```csharp
public IPromise ContinueWith(Func<IPromise> onResolved)
```

## See Also

* interface [IPromise](./IPromise.md)
* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

---
   
   
# Id property

ID of the promise, useful for debugging.

```csharp
public Snowflake Id { get; }
```

## See Also

* struct [Snowflake](../../Entities/Snowflake.md)
* interface [IPromise](./IPromise.md)
* namespace [Oxide.Ext.Discord.Interfaces.Promises](./PromisesNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
