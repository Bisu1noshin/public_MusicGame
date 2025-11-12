#version 330
//
//shader.vert
//

uniform mat4 WorldTransform;
uniform mat4 ViewProj;
in vec3 inPosition;

void main(void){
	vec4 pos = vec4(inPosition, 1.0);
	gl_Position = pos * WorldTransform * ViewProj;
}