#pragma once
#include <cmath>
#include "GL/glew.h"

#ifndef MY_MATH
#define MY_MATH
#endif

/// <summary>
/// 座標系をカバーするヘッダ
/// </summary>

const float  pi = atanf(1) * 4;
const float DegtoRad = pi / 180.f;
const float RadtoDeg = 180.f / pi;


/// <summary>
/// 2次元座標
/// </summary>
class Vector2 {
public:
	float x, y;

	Vector2(float x_, float y_) : x(x_), y(y_) {}
	Vector2(int x_, int y_) : x(float(x_)), y(float(y_)) {}
	Vector2() : x(0), y(0) {}
	Vector2(const class Vector3& v);
	inline static float sin(const Vector2& v) { return sinf(atan(v)); }
	inline static float cos(const Vector2& v) { return cosf(atan(v)); }
	inline static float atan(const Vector2& v) { return atan2(v.y, v.x); }

	inline static float addSin(float origin, float add) { //反時計回りの回転
		return sinf(origin) * cosf(add * DegtoRad) + cosf(origin) * sinf(add * DegtoRad);
	}
	inline static float addCos(float origin, float add) { //反時計回りの回転
		return cosf(origin) * cosf(add * DegtoRad) - sinf(origin) * sinf(add * DegtoRad);
	}
	inline static float addSin(const Vector2& origin, float add) { //反時計回りの回転
		return sin(origin) * cosf(add * DegtoRad) + cos(origin) * sinf(add * DegtoRad);
	}
	inline static float addCos(const Vector2& origin, float add) { //反時計回りの回転
		return cos(origin) * cosf(add * DegtoRad) - sin(origin) * sinf(add * DegtoRad);
	}

	Vector2 OffsetCopy(Vector2 other) const {
		return *this + other;
	}
	Vector2 OffsetCopy(float x_, float y_) const {
		return Vector2(x + x_, y + y_);
	}
	Vector2 OffsetCopy(int x_, int y_) const {
		return OffsetCopy((float)x_, (float)y_);
	}
	void Offset(Vector2 other) {
		*this = OffsetCopy(other);
	}
	void Offset(float x_, float y_) {
		*this = OffsetCopy(x_, y_);
	}

	inline float Length() const {
		return sqrt(x * x + y * y);
	}
	inline float Lengthsqr() const {
		return x * x + y * y;
	}
	inline Vector2 Normalized() const {
		return Vector2(x / Length(), y / Length());
	}
	inline void Normalize() {
		*this = Normalized();
	}
	bool operator==(const Vector2& other) const {
		return x == other.x && y == other.y;
	}

	Vector2 operator+=(const Vector2& other) {
		x += other.x, y += other.y;
		return *this;
	}
	Vector2 operator-=(const Vector2& other) {
		x -= other.x, y -= other.y;
		return *this;
	}
	Vector2 operator+(const Vector2& other) const { return Vector2(x + other.x, y + other.y); }
	Vector2 operator-(const Vector2& other) const { return Vector2(x - other.x, y - other.y); }
	Vector2 operator*(float f) const { return Vector2(x * f, y * f); }
	Vector2 operator/(float f) const { return Vector2(x / f, y / f); }

	inline static float Dot(const Vector2& v1, const Vector2& v2) { //内積
		return v1.x * v2.x + v1.y * v2.y;
	}
	inline static float AngleOf(const Vector2& v1, const Vector2& v2) { //角度値
		return acosf(Dot(v1, v2) / (v1.Length() * v2.Length())) * RadtoDeg;
	}
	static const Vector2 UnitX, UnitY;
};

/// <summary>
/// ３次元座標
/// </summary>
class Vector3 {
public:
	float x, y, z;
	Vector3() : x(0), y(0), z(0) {}
	Vector3(Vector2 v) : x(v.x), y(v.y), z(0) {}
	Vector3(float x, float y, float z) : x(x), y(y), z(z) {}
	Vector3(int x, int y, int z) : x((float)x), y((float)y), z((float)z) {}
	void Offset(Vector3 other) {
		x += other.x, y += other.y, z += other.z;
	}
	inline float Length() const {
		return sqrt(x * x + y * y + z * z);
	}
	inline float Lengthsqr() const {
		return x * x + y * y + z * z;
	}
	Vector3 Normalized() const {
		return Vector3(x / Length(), y / Length(), z / Length());
	}
	bool operator==(const Vector3& other) const { return (x == other.x && y == other.y && z == other.z); }
	Vector3 operator+(const Vector3& other) const { return Vector3(x + other.x, y + other.y, z + other.z); }
	Vector3 operator-(const Vector3& other) const { return Vector3(x - other.x, y - other.y, z - other.z); }
	Vector3 operator*(float f) const { return Vector3(x * f, y * f, z * f); }
	Vector3 operator/(float f) const { return Vector3(x / f, y / f, z / f); }
	Vector3 operator+=(const Vector3& other) {
		*this = *this + other;
		return *this;
	}
	Vector3 operator-=(const Vector3& other) {
		*this = *this - other;
		return *this;
	}
	Vector3 operator*=(float f) {
		x *= f; y *= f; z *= f;
		return *this;
	}
	Vector3& operator/=(float f) {
		x /= f; y /= f; z /= f;
		return *this;
	}
	inline static float Dot(const Vector3& v1, const Vector3& v2) { //２つのベクトルの内積を返す
		return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
	}
	inline static float AngleOf(const Vector3& v1, const Vector3& v2) { //2つのベクトル間の内角を角度値で返す
		return acosf(Dot(v1, v2) / (v1.Length() * v2.Length())) * RadtoDeg;
	}
	inline static Vector3 Cross(const Vector3& v1, const Vector3& v2) { //２つのベクトルの外積を返す
		return Vector3(v1.y * v2.z - v1.z * v2.y,
			v1.z * v2.x - v1.x * v2.z,
			v1.x * v2.y - v1.y * v2.x);
	}
	inline bool IsSameDir(const Vector3& other) const {
		return this->Normalized() == other.Normalized();
	}
	static Vector3 Transform(const Vector3& lastVec, const class Quaternion& nextVec); //ベクトルを単位クォータニオンで回転させる
	static const Vector3 UnitX, UnitY, UnitZ;
};

/// <summary>
/// クォータニオン
/// 回転に使う
/// </summary>
class Quaternion {
public:
	Vector3 v; float s;
	Quaternion() : v(), s(1) {} //デフォルトは単位クォータニオンと同じ
	Quaternion(const Vector3& v_, float s_) : v(v_), s(s_) {}
	Quaternion(float x, float y, float z, float w) : v(x, y, z), s(w) {}
	static Quaternion FromEulerAngles(const Vector3& euler);
	static Quaternion Slerp(const Quaternion& a, const Quaternion& b, float f);
	static Quaternion Concatenate(const Quaternion& q1, const Quaternion& q2);
	static Vector3 ToEulerAngles(const Quaternion& q);
	Quaternion Normalized() const;
	void Normalize() { *this = Normalized(); }
	Quaternion Conjugate() const;
	Quaternion Inverse() const;
	static const Quaternion identity;
	Quaternion operator*(float f) const {
		return Quaternion(v * f, s * f);
	}
	Quaternion operator*=(float f) {
		v *= f; s *= f;
		return *this;
	}
	Quaternion operator/(float f) const {
		return Quaternion(v / f, s / f);
	}
};


/// <summary>
/// シンプルな四角
/// 一応作った
/// </summary>
class Rect {
public:
	int x, y, w, h;

	Rect OffsetCopy(const Vector2& v) const {
		return Rect(x + (int)v.x, y + (int)v.y, w, h);
	}
	Rect OffsetCopy(int x_, int y_) const {
		return Rect(x + x_, y + y_, w, h);
	}
	Rect OffsetCopy(float x_, float y_) const {
		return Rect(x + (int)x_, y + (int)y_, w, h);
	}
	void Offset(const Vector2& v) {
		*this = OffsetCopy(v);
	}
	void Offset(int x_, int y_) {
		*this = OffsetCopy(x_, y_);
	}
	void Offset(float x_, float y_) {
		*this = OffsetCopy(x_, y_);
	}

	bool Hit(const Rect& other) const {
		if ((x <= other.x && x + w >= other.x) || (x >= other.x && other.x + other.w >= x)) {
			if ((y <= other.y && y + h >= other.y) || (y >= other.y && other.y + other.h >= y)) {
				return true;
			}
		}
		return false;
	}

	bool Contain(const Vector2& point) const {
		if (x <= point.x && x + w >= point.x) {
			if (y <= point.y && y + h >= point.y) {
				return true;
			}
		}
		return false;
	}

	Rect() : x(0), y(0), w(0), h(0) {}
	Rect(int x_, int y_, int w_, int h_) : x(x_), y(y_), w(w_), h(h_) {}
};

/// <summary>
/// OpenGLで使う
/// </summary>
class Color {
public:
	GLfloat r, g, b, a;
	Color() : r(1), g(1), b(1), a(1) {}
	Color(GLfloat r, GLfloat g, GLfloat b, GLfloat a) : r(r), g(g), b(b), a(a) {}
	
};

/// <summary>
/// 4x4の単位行列
/// </summary>
class mat4 {
public:
	float arr[16] = { 1.0f, 0.0f, 0.0f, 0.0f,
					  0.0f, 1.0f, 0.0f, 0.0f,
					  0.0f, 0.0f, 1.0f, 0.0f,
					  0.0f, 0.0f, 0.0f, 1.0f };
	const float size;
	mat4() : size(16) {}
	mat4(float* f_) : size(16) { //既存の配列のコピー
		for (int i = 0; i < 16; ++i) {
			arr[i] = *(f_ + i);
		}
	}
	mat4(mat4& other) : size(16) {
		for (int i = 0; i < 16; ++i) arr[i] = other[i];
	}
	mat4(const mat4& other) : size(16) {
		for (int i = 0; i < 16; ++i) arr[i] = other[i];
	}

	static mat4 CreateMatrixfor(const Vector2& vec);
	static mat4 CreateMatrixfor(const Vector3& vec);
	static mat4 CreateMatrixfor(float m1, float m2, float m3);
	static mat4 CreateMatrixfor(float m1, float m2);
	static mat4 CreateMatrixfor(int i1, int i2, int i3 = 0) { return CreateMatrixfor((float)i1, (float)i2, (float)i3); }

	static mat4 CreateTranslationMatrixfor(const Vector3& vec);
	static mat4 CreateRotationZMatrixfor(float f_);
	static mat4 CreateMatrixfromQuaternion(const Quaternion& q);

	const float* GetAsFloatPtr() const;
	void ChangeElements(float e1, float e2, float e3 = 1.0f); //拡縮等
	void Translation(float t1, float t2, float t3 = 0.0f); //平行移動
	void Translation(int i1, int i2, int i3 = 0) { Translation(Vector3(i1, i2, i3)); }
	void Translation(const Vector3& v);

	float& operator[](int i) { return arr[i]; }
	const float operator[](int i) const { return arr[i]; }

	mat4& operator=(const mat4& other) {
		for (int i = 0; i < 16; ++i) {
			arr[i] = other.arr[i];
		}
		return *this;
	}
	mat4 operator*(const mat4& other) const {
		mat4 rMat;
		for (int y = 0; y < 4; ++y) {
			for (int x = 0; x < 4; ++x) {
				float num = 0;
				for (int w = 0; w < 4; ++w) {
					num += arr[y * 4 + w] * other[w * 4 + x];
				}
				rMat[y * 4 + x] = num;
			}
		}
		return rMat;
	}
	mat4& operator*=(const mat4& other) {
		*this = *this * other;
		return *this;
	}

};

/// <summary>
/// ベーシックな線分
/// </summary>
class LineSeg {
public:
	Vector2 start, end;
	Vector2 PointOn(float f) const {
		return start + (end - start) * f;
	}
	float MinDistSqr(const Vector2& point) const {
		Vector2 ab = end - start;
		Vector2 ba = ab * -1.0f;
		Vector2 ac = point - start;
		Vector2 bc = point - end;

		if (Vector2::Dot(ab, ac) < 0.0f) {
			return ac.Lengthsqr();
		}
		else if (Vector2::Dot(ba, bc) < 0.0f) {
			return bc.Lengthsqr();
		}
		else {
			float scalar = Vector2::Dot(ac, ab) / Vector2::Dot(ab, ab);
			Vector2 p = ab * scalar;
			return (ac - p).Lengthsqr();
		}
	}
};