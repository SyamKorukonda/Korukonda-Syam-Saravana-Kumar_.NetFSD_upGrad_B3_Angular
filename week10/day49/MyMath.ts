// Named Export 
export const PI = 3.14;
export const MAX = 100;

export function add(a: number, b: number): number {
  return a + b;
}

export function sub(a: number, b: number): number {
  return a - b;
}

export default function greet(){
  return "Greeting message from MyMath Module";
}
