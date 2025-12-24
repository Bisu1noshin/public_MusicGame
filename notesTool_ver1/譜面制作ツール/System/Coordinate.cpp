#include "Coordinate.h"
#include "Math.h"

Vector2::Vector2(const Vector3& v) : x(v.x), y(v.y) {}

Vector3 Vector3::Transform(const Vector3& lastVec, const Quaternion& nextVec) {
	Quaternion r(lastVec, 0);
	Quaternion r_ = Quaternion::Concatenate(Quaternion::Concatenate(r, nextVec), nextVec.Inverse());
	return r_.v;
}

Quaternion Quaternion::Normalized() const {
	float norm = sqrtf(v.x * v.x + v.y * v.y + v.z * v.z + s * s);
	if (norm == 0.0f) {
		return identity;
	}
	return Quaternion(v / norm, s / norm);
}
Quaternion Quaternion::Conjugate() const { //逆元のクォータニオン
	return Quaternion(v * -1.0f, s);
}
Quaternion Quaternion::Inverse() const { //逆元の単位クォータニオン
	return Conjugate() / (s * s + v.x * v.x + v.y * v.y + v.z * v.z);
}
Quaternion Quaternion::Concatenate(const Quaternion& q1, const Quaternion& q2) { //クォータニオン同士の連結 q1から回転させる
	Quaternion c;
	c.v = q1.v * q2.s + q2.v * q1.s + Vector3::Cross(q2.v, q1.v);
	c.s = q1.s * q2.s - Vector3::Dot(q2.v, q1.v);
	return c;
}
Quaternion Quaternion::FromEulerAngles(const Vector3& euler) { //オイラー角からクォータニオンへの変換
	float cz = cosf(euler.z * 0.5f), sz = sinf(euler.z * 0.5f);
	float cy = cosf(euler.y * 0.5f), sy = sinf(euler.y * 0.5f);
	float cx = cosf(euler.x * 0.5f), sx = sinf(euler.x * 0.5f);
	Quaternion q;
	q.s = cz * cy * cx + sz * sy * sx;
	q.v.x = cz * cy * cx - sz * sy * sx;
	q.v.y = cz * sy * cx + sz * cy * sx;
	q.v.z = sz * cy * cx - cz * sy * sx;
	return q.Normalized();
}
Vector3 Quaternion::ToEulerAngles(const Quaternion& q) {
	float x = atan2f(2 * (q.v.x * q.s + q.v.y * q.v.z), 1 - 2 * (q.v.x * q.v.x + q.v.y * q.v.y));
	float y = asinf(2 * (q.s * q.v.y - q.v.z * q.v.x));
	float z = atan2f(2 * (q.s * q.v.z + q.v.x * q.v.y), 1 - 2 * (q.v.y * q.v.y + q.v.z * q.v.z));
	return Vector3(x, y, z);
}
Quaternion Quaternion::Slerp(const Quaternion& a, const Quaternion& b, float f) { //球面線形補完
	if (fabsf(f) > 1.0f) {
		f /= fabsf(f);
	}
	Quaternion c = b;
	float dot = Vector3::Dot(a.v, c.v) + a.s * c.s; //クォータニオン同士のドット積
	if (dot < 0.0f) {
		c *= -1.0f;
		dot *= -1.0f;
	}
	if (Math::NearZero(1.0f - dot)) {
		Quaternion r(
			a.v.x + (c.v.x - a.v.x) * f,
			a.v.y + (c.v.y - a.v.y) * f,
			a.v.z + (c.v.z - a.v.z) * f,
			a.s + (c.s - a.s) * f);
		return r.Normalized();
	}
	float theta = acosf(Math::Max(-1.0f, Math::Min(1.0f, dot)));
	float w1 = sinf((1.0f - f) * theta) / sinf(theta);
	float w2 = sinf(f * theta) / sinf(theta);
	Quaternion r(
		a.v.x * w1 + c.v.x * w2,
		a.v.y * w1 + c.v.y * w2,
		a.v.z * w1 + c.v.z * w2,
		a.s * w1 + c.s * w2
	);
	return r.Normalized();
}


mat4 mat4::CreateMatrixfor(const Vector2& vec) {
	mat4 rMat;
	rMat.ChangeElements(vec.x, vec.y);
	return rMat;
}
mat4 mat4::CreateMatrixfor(float m1, float m2, float m3) {
	mat4 rMat;
	rMat.ChangeElements(m1, m2, m3);
	return rMat;
}
mat4 mat4::CreateMatrixfor(float m1, float m2) {
	return CreateMatrixfor(m1, m2, 1.0f);
}
mat4 mat4::CreateMatrixfor(const Vector3& vec) {
	mat4 rMat;
	rMat.ChangeElements(vec.x, vec.y, vec.z);
	return rMat;
}
mat4 mat4::CreateRotationZMatrixfor(float f_) {
	mat4 rMat;
	rMat[0] = cosf(f_), rMat[1] = sinf(f_);
	rMat[4] = -sinf(f_), rMat[5] = cosf(f_);
	return rMat;
}
mat4 mat4::CreateTranslationMatrixfor(const Vector3& vec) {
	mat4 rMat;
	rMat.Translation(vec);
	return rMat;
}

const float* mat4::GetAsFloatPtr() const {
	const float* arr_ = arr;
	return arr_;
}
void mat4::ChangeElements(float e1, float e2, float e3) {
	arr[0] = e1, arr[5] = e2, arr[10] = e3;
}
mat4 mat4::CreateMatrixfromQuaternion(const Quaternion& q) {
	mat4 r;
	r[0] = 1 - 2 * q.v.y * q.v.y - 2 * q.v.z * q.v.z;
	r[1] = 2 * q.v.x * q.v.y + 2 * q.s * q.v.z;
	r[2] = 2 * q.v.x * q.v.z - 2 * q.s * q.v.y;
	r[4] = 2 * q.v.x * q.v.y - 2 * q.s * q.v.z;
	r[5] = 1 - 2 * q.v.x * q.v.x - 2 * q.v.z * q.v.z;
	r[6] = 2 * q.v.y * q.v.z + 2 * q.s * q.v.x;
	r[8] = 2 * q.v.x * q.v.z + 2 * q.s * q.v.y;
	r[9] = 2 * q.v.y * q.v.z - 2 * q.s * q.v.x;
	r[10] = 1 - 2 * q.v.x * q.v.x - 2 * q.v.y * q.v.y;
	return r;
}
void mat4::Translation(float t1, float t2, float t3) {
	arr[12] = t1, arr[13] = t2, arr[14] = t3;
}
void mat4::Translation(const Vector3& v) {
	Translation(v.x, v.y, v.z);
}