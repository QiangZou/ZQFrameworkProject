// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Proto/ExampleConfig1.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Proto/ExampleConfig1.proto</summary>
public static partial class ExampleConfig1Reflection {

  #region Descriptor
  /// <summary>File descriptor for Proto/ExampleConfig1.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static ExampleConfig1Reflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChpQcm90by9FeGFtcGxlQ29uZmlnMS5wcm90byKcAQoORXhhbXBsZUNvbmZp",
          "ZzESCgoCaWQYASABKAUSDAoEbmFtZRgCIAEoCRIRCglmbG9hdFR5cGUYAyAB",
          "KAISEAoIYm9vbFR5cGUYBCABKAgSEAoIdWludFR5cGUYBSADKA0SEQoJaW50",
          "NjRUeXBlGAYgAygDEhIKCnVpbnQ2NFR5cGUYByABKAQSEgoKZG91YmxlVHlw",
          "ZRgIIAEoAWIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::ExampleConfig1), global::ExampleConfig1.Parser, new[]{ "Id", "Name", "FloatType", "BoolType", "UintType", "Int64Type", "Uint64Type", "DoubleType" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class ExampleConfig1 : pb::IMessage<ExampleConfig1>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<ExampleConfig1> _parser = new pb::MessageParser<ExampleConfig1>(() => new ExampleConfig1());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ExampleConfig1> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ExampleConfig1Reflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ExampleConfig1() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ExampleConfig1(ExampleConfig1 other) : this() {
    id_ = other.id_;
    name_ = other.name_;
    floatType_ = other.floatType_;
    boolType_ = other.boolType_;
    uintType_ = other.uintType_.Clone();
    int64Type_ = other.int64Type_.Clone();
    uint64Type_ = other.uint64Type_;
    doubleType_ = other.doubleType_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ExampleConfig1 Clone() {
    return new ExampleConfig1(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private int id_;
  /// <summary>
  ///字段备注0
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "name" field.</summary>
  public const int NameFieldNumber = 2;
  private string name_ = "";
  /// <summary>
  ///字段备注1
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Name {
    get { return name_; }
    set {
      name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "floatType" field.</summary>
  public const int FloatTypeFieldNumber = 3;
  private float floatType_;
  /// <summary>
  ///字段备注2
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float FloatType {
    get { return floatType_; }
    set {
      floatType_ = value;
    }
  }

  /// <summary>Field number for the "boolType" field.</summary>
  public const int BoolTypeFieldNumber = 4;
  private bool boolType_;
  /// <summary>
  ///字段备注3
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool BoolType {
    get { return boolType_; }
    set {
      boolType_ = value;
    }
  }

  /// <summary>Field number for the "uintType" field.</summary>
  public const int UintTypeFieldNumber = 5;
  private static readonly pb::FieldCodec<uint> _repeated_uintType_codec
      = pb::FieldCodec.ForUInt32(42);
  private readonly pbc::RepeatedField<uint> uintType_ = new pbc::RepeatedField<uint>();
  /// <summary>
  ///字段备注4
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<uint> UintType {
    get { return uintType_; }
  }

  /// <summary>Field number for the "int64Type" field.</summary>
  public const int Int64TypeFieldNumber = 6;
  private static readonly pb::FieldCodec<long> _repeated_int64Type_codec
      = pb::FieldCodec.ForInt64(50);
  private readonly pbc::RepeatedField<long> int64Type_ = new pbc::RepeatedField<long>();
  /// <summary>
  ///字段备注5
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<long> Int64Type {
    get { return int64Type_; }
  }

  /// <summary>Field number for the "uint64Type" field.</summary>
  public const int Uint64TypeFieldNumber = 7;
  private ulong uint64Type_;
  /// <summary>
  ///字段备注6
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ulong Uint64Type {
    get { return uint64Type_; }
    set {
      uint64Type_ = value;
    }
  }

  /// <summary>Field number for the "doubleType" field.</summary>
  public const int DoubleTypeFieldNumber = 8;
  private double doubleType_;
  /// <summary>
  ///字段备注7
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public double DoubleType {
    get { return doubleType_; }
    set {
      doubleType_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as ExampleConfig1);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ExampleConfig1 other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (Name != other.Name) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(FloatType, other.FloatType)) return false;
    if (BoolType != other.BoolType) return false;
    if(!uintType_.Equals(other.uintType_)) return false;
    if(!int64Type_.Equals(other.int64Type_)) return false;
    if (Uint64Type != other.Uint64Type) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(DoubleType, other.DoubleType)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (Name.Length != 0) hash ^= Name.GetHashCode();
    if (FloatType != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(FloatType);
    if (BoolType != false) hash ^= BoolType.GetHashCode();
    hash ^= uintType_.GetHashCode();
    hash ^= int64Type_.GetHashCode();
    if (Uint64Type != 0UL) hash ^= Uint64Type.GetHashCode();
    if (DoubleType != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(DoubleType);
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (FloatType != 0F) {
      output.WriteRawTag(29);
      output.WriteFloat(FloatType);
    }
    if (BoolType != false) {
      output.WriteRawTag(32);
      output.WriteBool(BoolType);
    }
    uintType_.WriteTo(output, _repeated_uintType_codec);
    int64Type_.WriteTo(output, _repeated_int64Type_codec);
    if (Uint64Type != 0UL) {
      output.WriteRawTag(56);
      output.WriteUInt64(Uint64Type);
    }
    if (DoubleType != 0D) {
      output.WriteRawTag(65);
      output.WriteDouble(DoubleType);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (FloatType != 0F) {
      output.WriteRawTag(29);
      output.WriteFloat(FloatType);
    }
    if (BoolType != false) {
      output.WriteRawTag(32);
      output.WriteBool(BoolType);
    }
    uintType_.WriteTo(ref output, _repeated_uintType_codec);
    int64Type_.WriteTo(ref output, _repeated_int64Type_codec);
    if (Uint64Type != 0UL) {
      output.WriteRawTag(56);
      output.WriteUInt64(Uint64Type);
    }
    if (DoubleType != 0D) {
      output.WriteRawTag(65);
      output.WriteDouble(DoubleType);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (Name.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
    }
    if (FloatType != 0F) {
      size += 1 + 4;
    }
    if (BoolType != false) {
      size += 1 + 1;
    }
    size += uintType_.CalculateSize(_repeated_uintType_codec);
    size += int64Type_.CalculateSize(_repeated_int64Type_codec);
    if (Uint64Type != 0UL) {
      size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uint64Type);
    }
    if (DoubleType != 0D) {
      size += 1 + 8;
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(ExampleConfig1 other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.Name.Length != 0) {
      Name = other.Name;
    }
    if (other.FloatType != 0F) {
      FloatType = other.FloatType;
    }
    if (other.BoolType != false) {
      BoolType = other.BoolType;
    }
    uintType_.Add(other.uintType_);
    int64Type_.Add(other.int64Type_);
    if (other.Uint64Type != 0UL) {
      Uint64Type = other.Uint64Type;
    }
    if (other.DoubleType != 0D) {
      DoubleType = other.DoubleType;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 29: {
          FloatType = input.ReadFloat();
          break;
        }
        case 32: {
          BoolType = input.ReadBool();
          break;
        }
        case 42:
        case 40: {
          uintType_.AddEntriesFrom(input, _repeated_uintType_codec);
          break;
        }
        case 50:
        case 48: {
          int64Type_.AddEntriesFrom(input, _repeated_int64Type_codec);
          break;
        }
        case 56: {
          Uint64Type = input.ReadUInt64();
          break;
        }
        case 65: {
          DoubleType = input.ReadDouble();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 29: {
          FloatType = input.ReadFloat();
          break;
        }
        case 32: {
          BoolType = input.ReadBool();
          break;
        }
        case 42:
        case 40: {
          uintType_.AddEntriesFrom(ref input, _repeated_uintType_codec);
          break;
        }
        case 50:
        case 48: {
          int64Type_.AddEntriesFrom(ref input, _repeated_int64Type_codec);
          break;
        }
        case 56: {
          Uint64Type = input.ReadUInt64();
          break;
        }
        case 65: {
          DoubleType = input.ReadDouble();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code