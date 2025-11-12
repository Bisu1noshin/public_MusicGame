#version 330

//
//shader.vert
//
in vec3 inPosition;
void main(void){
	gl_Position = vec4(inPosition, 1.0);
}