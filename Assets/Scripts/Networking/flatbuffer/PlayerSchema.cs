// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

using global::System;
using global::FlatBuffers;

public struct UnitPosition : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static UnitPosition GetRootAsUnitPosition(ByteBuffer _bb) { return GetRootAsUnitPosition(_bb, new UnitPosition()); }
  public static UnitPosition GetRootAsUnitPosition(ByteBuffer _bb, UnitPosition obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public UnitPosition __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public float X { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float Y { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }

  public static Offset<UnitPosition> CreateUnitPosition(FlatBufferBuilder builder,
      float x = 0.0f,
      float y = 0.0f) {
    builder.StartObject(2);
    UnitPosition.AddY(builder, y);
    UnitPosition.AddX(builder, x);
    return UnitPosition.EndUnitPosition(builder);
  }

  public static void StartUnitPosition(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddX(FlatBufferBuilder builder, float x) { builder.AddFloat(0, x, 0.0f); }
  public static void AddY(FlatBufferBuilder builder, float y) { builder.AddFloat(1, y, 0.0f); }
  public static Offset<UnitPosition> EndUnitPosition(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<UnitPosition>(o);
  }
};

public struct UnitFire : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static UnitFire GetRootAsUnitFire(ByteBuffer _bb) { return GetRootAsUnitFire(_bb, new UnitFire()); }
  public static UnitFire GetRootAsUnitFire(ByteBuffer _bb, UnitFire obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public UnitFire __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public float X { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float Y { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float MouseX { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float MouseY { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public bool IsSpecial { get { int o = __p.__offset(12); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<UnitFire> CreateUnitFire(FlatBufferBuilder builder,
      float x = 0.0f,
      float y = 0.0f,
      float mouseX = 0.0f,
      float mouseY = 0.0f,
      bool isSpecial = false) {
    builder.StartObject(5);
    UnitFire.AddMouseY(builder, mouseY);
    UnitFire.AddMouseX(builder, mouseX);
    UnitFire.AddY(builder, y);
    UnitFire.AddX(builder, x);
    UnitFire.AddIsSpecial(builder, isSpecial);
    return UnitFire.EndUnitFire(builder);
  }

  public static void StartUnitFire(FlatBufferBuilder builder) { builder.StartObject(5); }
  public static void AddX(FlatBufferBuilder builder, float x) { builder.AddFloat(0, x, 0.0f); }
  public static void AddY(FlatBufferBuilder builder, float y) { builder.AddFloat(1, y, 0.0f); }
  public static void AddMouseX(FlatBufferBuilder builder, float mouseX) { builder.AddFloat(2, mouseX, 0.0f); }
  public static void AddMouseY(FlatBufferBuilder builder, float mouseY) { builder.AddFloat(3, mouseY, 0.0f); }
  public static void AddIsSpecial(FlatBufferBuilder builder, bool isSpecial) { builder.AddBool(4, isSpecial, false); }
  public static Offset<UnitFire> EndUnitFire(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<UnitFire>(o);
  }
};

