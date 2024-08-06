# Promise&lt;TPromised&gt; class

Implements a C# promise. https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise

```csharp
public sealed class Promise<TPromised> : BasePromise, IPendingPromise<TPromised>
```

## Public Members

| name | description |
| --- | --- |
| [Promise](#promise&amp;lt;tpromised&amp;gt;-constructor)() | Constructor |
| static [Create](#create-method)() | Returns a promise that is currently pending |
| [Catch](#catch-method-1-of-2)(…) |  (2 methods) |
| [Catch&lt;TException&gt;](#catch&amp;lt;texception&amp;gt;-method)(…) |  |
| [ContinueWith](#continuewith-method)(…) |  |
| [ContinueWith&lt;TConvert&gt;](#continuewith&amp;lt;tconvert&amp;gt;-method)(…) |  |
| [Finally](#finally-method)(…) |  |
| [GetAwaiter](#getawaiter-method)() |  |
| override [Reject](#reject-method)(…) |  |
| [Resolve](#resolve-method)(…) |  |
| [Then](#then-method-1-of-4)(…) |  (4 methods) |
| [Then&lt;TConvert&gt;](#then&amp;lt;tconvert&amp;gt;-method-1-of-3)(…) |  (3 methods) |
| [ThenAll](#thenall-method)(…) |  |
| [ThenAll&lt;TConvert&gt;](#thenall&amp;lt;tconvert&amp;gt;-method)(…) |  |
| static [All](#all-method-1-of-2)(…) | Returns a promise that resolves when all the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results. (2 methods) |
| static [Create](#create-method)(…) |  |
| static [Create&lt;TConvert&gt;](#create&amp;lt;tconvert&amp;gt;-method)() | Returns a promise that is currently pending |
| static [Rejected](#rejected-method)(…) | Convert an exception directly into a rejected promise. |
| static [Resolved](#resolved-method)(…) | Convert a simple value directly into a resolved promise. |

## Protected Members

| name | description |
| --- | --- |
| override [ClearHandlers](#clearhandlers-method)() |  |
| override [EnterPool](#enterpool-method)() |  |

## See Also

* class [BasePromise](./BasePromise.md)
* interface [IPendingPromise&lt;TPromised&gt;](../Interfaces/IPendingPromise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [Promise.cs](../../../../Oxide.Ext.Discord/Types/Promise.cs)
   
   
# Create method (1 of 3)

Returns a promise that is currently pending

```csharp
public static Promise Create()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Create method (2 of 3)

```csharp
public static IPromise<TPromised> Create(Action<Action<TPromised>, Action<Exception>> resolver)
```

| parameter | description |
| --- | --- |
| resolver |  |

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Create&lt;TConvert&gt; method (3 of 3)

Returns a promise that is currently pending

```csharp
public static Promise<TConvert> Create<TConvert>()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Resolved method

Convert a simple value directly into a resolved promise.

```csharp
public static IPromise<TPromised> Resolved(TPromised promisedValue)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Rejected method

Convert an exception directly into a rejected promise.

```csharp
public static IPromise<TPromised> Rejected(Exception ex)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Resolve method

```csharp
public void Resolve(TPromised value)
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Reject method

```csharp
public override void Reject(Exception ex)
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Catch method (1 of 3)

```csharp
public IPromise Catch(Action<Exception> onRejected)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Catch method (2 of 3)

```csharp
public IPromise<TPromised> Catch(Func<Exception, TPromised> onRejected)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Catch&lt;TException&gt; method (3 of 3)

```csharp
public IPromise Catch<TException>(Action<TException> onRejected)
    where TException : Exception
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Then method (1 of 7)

```csharp
public IPromise<TPromised> Then(Action<TPromised> onResolved)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (2 of 7)

```csharp
public IPromise<TPromised> Then(IPromise<TPromised> promise)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (3 of 7)

```csharp
public IPromise<TPromised> Then(Action<TPromised> onResolved, Action<Exception> onRejected)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then method (4 of 7)

```csharp
public IPromise Then(Func<TPromised, IPromise> onResolved, Action<Exception> onRejected)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (5 of 7)

```csharp
public IPromise<TConvert> Then<TConvert>(Func<TPromised, IPromise<TConvert>> onResolved)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (6 of 7)

```csharp
public IPromise<TConvert> Then<TConvert>(Func<TPromised, TConvert> transform)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# Then&lt;TConvert&gt; method (7 of 7)

```csharp
public IPromise<TConvert> Then<TConvert>(Func<TPromised, IPromise<TConvert>> onResolved, 
    Func<Exception, IPromise<TConvert>> onRejected)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ThenAll method (1 of 2)

```csharp
public IPromise ThenAll(Func<TPromised, IEnumerable<IPromise>> chain)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# ThenAll&lt;TConvert&gt; method (2 of 2)

```csharp
public IPromise<IEnumerable<TConvert>> ThenAll<TConvert>(
    Func<TPromised, IEnumerable<IPromise<TConvert>>> chain)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# All method (1 of 2)

Returns a promise that resolves when all of the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results.

```csharp
public static IPromise<IEnumerable<TPromised>> All(
    IEnumerable<IPromise<TPromised>> promisesEnumerable)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# All method (2 of 2)

Returns a promise that resolves when all the promises in the enumerable argument have resolved. Returns a promise of a collection of the resolved results.

```csharp
public static IPromise<IEnumerable<TPromised>> All(params IPromise<TPromised>[] promises)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Finally method

```csharp
public IPromise<TPromised> Finally(Action onComplete)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ContinueWith method (1 of 2)

```csharp
public IPromise ContinueWith(Func<IPromise> onComplete)
```

## See Also

* interface [IPromise](../Interfaces/IPromise.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

---

# ContinueWith&lt;TConvert&gt; method (2 of 2)

```csharp
public IPromise<TConvert> ContinueWith<TConvert>(Func<IPromise<TConvert>> onComplete)
```

## See Also

* interface [IPromise&lt;TPromised&gt;](../Interfaces/IPromise%7BTPromised%7D.md)
* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# GetAwaiter method

```csharp
public ValueTaskAwaiter<TPromised> GetAwaiter()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ClearHandlers method

```csharp
protected override void ClearHandlers()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# EnterPool method

```csharp
protected override void EnterPool()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# Promise&lt;TPromised&gt; constructor

Constructor

```csharp
public Promise()
```

## See Also

* class [Promise&lt;TPromised&gt;](./Promise%7BTPromised%7D.md)
* namespace [Oxide.Ext.Discord.Types](./TypesNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->
