using UnityEngine;

[System.Serializable]
public struct IntVector2
{
  public int x, y;

  public IntVector2(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public static IntVector2 zero
  {
    get
    {
      return new IntVector2(0, 0);
    }
  }

  public static IntVector2 operator +(IntVector2 a, IntVector2 b)
  {
    return new IntVector2(a.x + b.x, a.y + b.y);
  }

  public static IntVector2 operator +(IntVector2 a, Vector2 b)
  {
    return new IntVector2(a.x + (int)b.x, a.y + (int)b.y);
  }

  public static IntVector2 operator -(IntVector2 a, IntVector2 b)
  {
    return new IntVector2(a.x - b.x, a.y - b.y);
  }

  public static IntVector2 operator -(IntVector2 a, Vector2 b)
  {
    return new IntVector2(a.x - (int)b.x, a.y - (int)b.y);
  }

  public static IntVector2 operator *(IntVector2 a, int b)
  {
    return new IntVector2(a.x * b, a.y * b);
  }
}