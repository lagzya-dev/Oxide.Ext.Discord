# Promise class

Implements a non-generic C# promise; this is a promise that simply resolves without delivering a value. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise

```csharp
public sealed class Promise : BasePromise, IPendingPromise
```

## Public Members

| name | description |
| --- | --- |
| [Promise](#promise-constructor)() | Constructor for the promise |
| static [Create](#create-method)() | Creates a Promise |
| [Catch](#catch-method)(…) |  |
| [Catch&lt;TException&gt;](#catch&amp;lt;texception&amp;gt;-method)(…) |  |
| [ContinueWith](#continuewith-method)(…) |  |
| [ContinueWith&lt;TConvert&gt;](#continuewith&amp;lt;tconvert&amp;gt;-method)(…) |  |
| [Finally](#finally-method)(…) |  |
| [GetAwaiter](#getawaiter-method)() |  |
| override [Reject](#reject-method)(…) |  |
| [Resolve](#resolve-method)() |  |
| [Then](#then-method-1-of-5)(…) |  (5 methods) |
| [Then&lt;TConvert&gt;](#then&amp;lt;tconvert&amp;gt;-method-1-of-2)(…) |  (2 methods) |
| [ThenAll](#thenall-method)(…) |  |
| [ThenAll&lt;TConvert&gt;](#thenall&amp;lt;tconvert&amp;gt;-method)(…) |  |
| [ThenSequence](#thensequence-method)(…) |  |
| static [All](#all-method-1-of-2)(…) | Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results. (2 methods) |
| static [Rejected](#rejected-method)(…) | Convert an exception directly into a rejected promise. |
| static [Resolved](#resolved-method)() | Returns a promise that has been resolved |
| static [Sequence](#sequence-method-1-of-2)(…) | Chain a number of operations using promises. Takes a number of functions each of which starts an async operation and yields a promise. (2 methods) |

## Protected Members

| name | description |
| --- | --- |
| override [ClearHandlers](#clearhandlers-method)() | Helper function clear out all handlers after resolution or rejection. |
| override [EnterPool](#enterpool-method)() |  |

## See Also

* class [BasePromise](./BasePromise.md)
* interface [IPendingPromise](../Interfaces/IPendingPromise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [Promise.cs](../../../../Oxide.Ext.Discord/Types/Promise.cs)
   
   
# Create method

Creates a Promise

```csharp
public static Promise Create()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Resolved method

Returns a promise that has been resolved

```csharp
public static IPromise Resolved()
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Rejected method

Convert an exception directly into a rejected promise.

```csharp
public static IPromise Rejected(Exception ex)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ClearHandlers method

Helper function clear out all handlers after resolution or rejection.

```csharp
protected override void ClearHandlers()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Resolve method

```csharp
public void Resolve()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Reject method

```csharp
public override void Reject(Exception ex)
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Catch method (1 of 2)

```csharp
public IPromise Catch(Action<Exception> onRejected)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Catch&lt;TException&gt; method (2 of 2)

```csharp
public IPromise Catch<TException>(Action<TException> onRejected)
    where TException : Exception
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Then method (1 of 7)

```csharp
public IPromise Then(Action onResolved)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (2 of 7)

```csharp
public IPromise Then(Func<IPromise> onResolved)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (3 of 7)

```csharp
public IPromise Then(IPromise promise)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (4 of 7)

```csharp
public IPromise Then(Action onResolved, Action<Exception> onRejected)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (5 of 7)

```csharp
public IPromise Then(Func<IPromise> onResolved, Action<Exception> onRejected)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (6 of 7)

```csharp
public IPromise<TConvert> Then<TConvert>(Func<IPromise<TConvert>> onResolved)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (7 of 7)

```csharp
public IPromise<TConvert> Then<TConvert>(Func<IPromise<TConvert>> onResolved, 
    Func<Exception, IPromise<TConvert>> onRejected)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ThenAll method (1 of 2)

```csharp
public IPromise ThenAll(Func<IEnumerable<IPromise>> chain)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# ThenAll&lt;TConvert&gt; method (2 of 2)

```csharp
public IPromise<IEnumerable<TConvert>> ThenAll<TConvert>(
    Func<IEnumerable<IPromise<TConvert>>> chain)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# All method (1 of 2)

Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results.

```csharp
public static IPromise All(IEnumerable<IPromise> promisesEnumerable)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# All method (2 of 2)

Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results.

```csharp
public static IPromise All(params IPromise[] promises)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ThenSequence method

```csharp
public IPromise ThenSequence(Func<IEnumerable<Func<IPromise>>> chain)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Sequence method (1 of 2)

Chain a number of operations using promises. Takes a number of functions each of which starts an async operation and yields a promise.

```csharp
public static IPromise Sequence(params Func<IPromise>[] fns)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Sequence method (2 of 2)

Chain a sequence of operations using promises. Takes a collection of functions each of which starts an async operation and yields a promise.

```csharp
public static IPromise Sequence(IEnumerable<Func<IPromise>> fns)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Finally method

```csharp
public IPromise Finally(Action onComplete)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ContinueWith method (1 of 2)

```csharp
public IPromise ContinueWith(Func<IPromise> onComplete)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# ContinueWith&lt;TConvert&gt; method (2 of 2)

```csharp
public IPromise<TConvert> ContinueWith<TConvert>(Func<IPromise<TConvert>> onComplete)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetAwaiter method

```csharp
public ValueTaskAwaiter GetAwaiter()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# EnterPool method

```csharp
protected override void EnterPool()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Promise constructor

Constructor for the promise

```csharp
public Promise()
```

## See Also

* class [Promise](./Promise.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
