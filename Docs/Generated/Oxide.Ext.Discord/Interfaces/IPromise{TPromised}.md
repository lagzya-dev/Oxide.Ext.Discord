# IPromise&lt;TPromised&gt; interface

Implements a C# promise. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise

```csharp
public interface IPromise<TPromised>
```

## Members

| name | description |
| --- | --- |
| [Id](#id-property) { get; } | ID of the promise, useful for debugging. |
| [Catch](#catch-method-1-of-2)(…) | Handle errors for the promise. (2 methods) |
| [Catch&lt;TException&gt;](#catch&amp;lt;texception&amp;gt;-method)(…) | Catches a specified exception |
| [ContinueWith](#continuewith-method)(…) | Add a callback that chains a non-value promise. ContinueWith callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The state of the returning promise will be based on the new non-value promise, not the preceding (rejected or resolved) promise. |
| [ContinueWith&lt;TConvert&gt;](#continuewith&amp;lt;tconvert&amp;gt;-method)(…) | Add a callback that chains a value promise (optionally converting to a different value type). ContinueWith callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The state of the returning promise will be based on the new value promise, not the preceding (rejected or resolved) promise. |
| [Finally](#finally-method)(…) | Add a finally callback. Finally callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The returned promise will be resolved or rejected, as per the preceding promise. |
| [Then](#then-method-1-of-4)(…) | Add a resolved callback. (4 methods) |
| [Then&lt;TConvert&gt;](#then&amp;lt;tconvert&amp;gt;-method-1-of-3)(…) | Add a resolved callback that chains a value promise (optionally converting to a different value type). (3 methods) |
| [ThenAll](#thenall-method)(…) | Chain an enumerable of promises, all of which must resolve. Converts to a non-value promise. The resulting promise is resolved when all of the promises have resolved. It is rejected as soon as any of the promises have been rejected. |
| [ThenAll&lt;TConvert&gt;](#thenall&amp;lt;tconvert&amp;gt;-method)(…) | Chain an enumerable of promises, all of which must resolve. Returns a promise for a collection of the resolved results. The resulting promise is resolved when all of the promises have resolved. It is rejected as soon as any of the promises have been rejected. |

## See Also

* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [IPromise.cs](../../../../Oxide.Ext.Discord/Interfaces/IPromise.cs)
   
   
# Catch method (1 of 3)

Handle errors for the promise.

```csharp
public IPromise Catch(Action<Exception> onRejected)
```

## See Also

* interface [IPromise](./IPromise.md)
* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Catch method (2 of 3)

Handle errors for the promise.

```csharp
public IPromise Catch(Func<Exception, TPromised> onRejected)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Catch&lt;TException&gt; method (3 of 3)

Catches a specified exception

```csharp
public IPromise Catch<TException>(Action<TException> onRejected)
    where TException : Exception
```

## See Also

* interface [IPromise](./IPromise.md)
* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Then method (1 of 7)

Add a resolved callback.

```csharp
public IPromise Then(Action<TPromised> onResolved)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (2 of 7)

```csharp
public IPromise Then(IPromise promise)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (3 of 7)

Add a resolved callback and a rejected callback.

```csharp
public IPromise Then(Action<TPromised> onResolved, Action<Exception> onRejected)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (4 of 7)

Add a resolved callback and a rejected callback. The resolved callback chains a non-value promise.

```csharp
public IPromise Then(Func<TPromised, IPromise> onResolved, Action<Exception> onRejected)
```

## See Also

* interface [IPromise](./IPromise.md)
* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (5 of 7)

Add a resolved callback that chains a value promise (optionally converting to a different value type).

```csharp
public IPromise<TConvert> Then<TConvert>(Func<TPromised, IPromise<TConvert>> onResolved)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (6 of 7)

Return a new promise with a different value. May also change the type of the value.

```csharp
public IPromise<TConvert> Then<TConvert>(Func<TPromised, TConvert> transform)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (7 of 7)

Add a resolved callback and a rejected callback. The resolved callback chains a value promise (optionally converting to a different value type).

```csharp
public IPromise<TConvert> Then<TConvert>(Func<TPromised, IPromise<TConvert>> onResolved, 
    Func<Exception, IPromise<TConvert>> onRejected)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ThenAll method (1 of 2)

Chain an enumerable of promises, all of which must resolve. Converts to a non-value promise. The resulting promise is resolved when all of the promises have resolved. It is rejected as soon as any of the promises have been rejected.

```csharp
public IPromise ThenAll(Func<TPromised, IEnumerable<IPromise>> chain)
```

## See Also

* interface [IPromise](./IPromise.md)
* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# ThenAll&lt;TConvert&gt; method (2 of 2)

Chain an enumerable of promises, all of which must resolve. Returns a promise for a collection of the resolved results. The resulting promise is resolved when all of the promises have resolved. It is rejected as soon as any of the promises have been rejected.

```csharp
public IPromise<IEnumerable<TConvert>> ThenAll<TConvert>(
    Func<TPromised, IEnumerable<IPromise<TConvert>>> chain)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Finally method

Add a finally callback. Finally callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The returned promise will be resolved or rejected, as per the preceding promise.

```csharp
public IPromise Finally(Action onComplete)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ContinueWith method (1 of 2)

Add a callback that chains a non-value promise. ContinueWith callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The state of the returning promise will be based on the new non-value promise, not the preceding (rejected or resolved) promise.

```csharp
public IPromise ContinueWith(Func<IPromise> onResolved)
```

## See Also

* interface [IPromise](./IPromise.md)
* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# ContinueWith&lt;TConvert&gt; method (2 of 2)

Add a callback that chains a value promise (optionally converting to a different value type). ContinueWith callbacks will always be called, even if any preceding promise is rejected, or encounters an error. The state of the returning promise will be based on the new value promise, not the preceding (rejected or resolved) promise.

```csharp
public IPromise<TConvert> ContinueWith<TConvert>(Func<IPromise<TConvert>> onComplete)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Id property

ID of the promise, useful for debugging.

```csharp
public Snowflake Id { get; }
```

## See Also

* struct [Snowflake](../Entities/Snowflake.md)
* interface [IPromise&lt;TPromised&gt;](./IPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Interfaces](./InterfacesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
