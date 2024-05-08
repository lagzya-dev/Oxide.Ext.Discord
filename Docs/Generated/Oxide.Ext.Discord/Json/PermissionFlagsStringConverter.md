# PermissionFlagsStringConverter class

Converts Permission Flags to and from a JSON string

```csharp
public class PermissionFlagsStringConverter : JsonConverter
```

## Public Members

| name | description |
| --- | --- |
| [PermissionFlagsStringConverter](#permissionflagsstringconverter-constructor)() | The default constructor. |
| override [CanConvert](#canconvert-method)(…) | Returns if the type equals PermissionFlags |
| override [ReadJson](#readjson-method)(…) | Converts the ulong JSON string to Permission Flags |
| override [WriteJson](#writejson-method)(…) | Writes Permission Flags as a JSON string |

## See Also

* namespace [Oxide.Ext.Discord.Json](./JsonNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [PermissionFlagsStringConverter.cs](../../../../Oxide.Ext.Discord/Json/PermissionFlagsStringConverter.cs)
   
   
# WriteJson method

Writes Permission Flags as a JSON string

```csharp
public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
```

| parameter | description |
| --- | --- |
| writer |  |
| value |  |
| serializer |  |

## See Also

* class [PermissionFlagsStringConverter](./PermissionFlagsStringConverter.md)
* namespace [Oxide.Ext.Discord.Json](./JsonNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# ReadJson method

Converts the ulong JSON string to Permission Flags

```csharp
public override object ReadJson(JsonReader reader, Type objectType, object existingValue, 
    JsonSerializer serializer)
```

| parameter | description |
| --- | --- |
| reader |  |
| objectType |  |
| existingValue |  |
| serializer |  |

## See Also

* class [PermissionFlagsStringConverter](./PermissionFlagsStringConverter.md)
* namespace [Oxide.Ext.Discord.Json](./JsonNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# CanConvert method

Returns if the type equals PermissionFlags

```csharp
public override bool CanConvert(Type objectType)
```

| parameter | description |
| --- | --- |
| objectType |  |

## See Also

* class [PermissionFlagsStringConverter](./PermissionFlagsStringConverter.md)
* namespace [Oxide.Ext.Discord.Json](./JsonNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
   
   
# PermissionFlagsStringConverter constructor

The default constructor.

```csharp
public PermissionFlagsStringConverter()
```

## See Also

* class [PermissionFlagsStringConverter](./PermissionFlagsStringConverter.md)
* namespace [Oxide.Ext.Discord.Json](./JsonNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->