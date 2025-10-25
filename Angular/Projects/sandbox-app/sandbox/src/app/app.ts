import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserInterface } from './modules/main/sandbox/models/user.model';
import { User } from "./modules/main/sandbox/components/user/user";
import { Observable } from '../../node_modules/rxjs/dist/types/index';
import { HttpClient } from '@angular/common/http';
import { interval, firstValueFrom } from 'rxjs';
import { ReactiveForm } from './modules/main/sandbox/components/reactive-form/reactive-form';

 type Visibility = 'visible' | 'hidden';
 
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, User, ReactiveForm],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  private localTestApiUrl = 'http://localhost:5208/Test';
  protected readonly title = signal('sandbox');
  city = `Vinnytsia`;
  isServerRunning = true;
  divVisibility: Visibility = 'visible';
  secretMessage = ``;

  constructor(private http: HttpClient) {}

  async changeCity() {
    this.city = `Kyiv`;
    this.title.set(`Kyiv signal`);
    this.isServerRunning = !this.isServerRunning;
    this.divVisibility = this.divVisibility === 'visible' ? 'hidden' : 'visible';
  }



  protected users : UserInterface[] = [
    {name: 'John', surname: 'Doe', age: 15, id: 0},
    {name: 'Alice', surname: 'Doe', age: 18, id: 1},
    {name: 'Bob', surname: 'Doe', age: 23, id: 2}
  ];

  hideSecreteMessage() {
    this.secretMessage = ``;
  }

  showSecreteMessage() {
    this.secretMessage = `THE SECRET MESSAGE`;
  }

  async getOne(functionId : number){

    console.log(`af${functionId}: getOne ${functionId}`);
    return 1; 
  }

  async getTwo(functionId : number){
    for(let i = 0;i < 5e9;i++) {} 
    console.log(`af${functionId}: getTwo`);
    return 1; 
  }

  async fastAwait(){
    return 1; 
  }


//------------------- notes ------------------
  async getValueFromApi(value: string): Promise<string> {
    const url = `${this.localTestApiUrl}?returnValue=${encodeURIComponent(value)}`;
    const response = await firstValueFrom(this.http.get(url, { responseType: 'text' }));
    console.log(`response received`);
    return response;
  }

  async asyncFunctionClick(functionId : number){
                                            
    setTimeout(() => console.log(`af${functionId}: setTimeout`), 0);
    console.log(`af${functionId}: A`); 
    for(let i = 0;i < 2e9;i++) {} 
    
    const res1 =  await this.getValueFromApi(functionId.toString());
    console.log(res1);


    for(let i = 0;i < 2e9;i++) {}     
    console.log(`af${functionId}: B`);  

    // const [res1, res2] = await Promise.all([
    //   firstValueFrom(this.getValueFromApi(functionId.toString())),
    //   firstValueFrom(this.getValueFromApi(functionId.toString() + '-2'))
    // ]);
    
    // console.log(res1, res2);
         
  }

  async printTwo(){ 
    for(let i = 0;i < 2e9; i++) {}; // -> Stack
    console.log(2);                 // -> Stack -> 3. 2
  }

  async printOne(){ 
    console.log(`Entered print one`);       // -> Stack -> 2. Entered print one
    Promise.resolve().then(() => console.log("Promise.then in one")); // -> microtask queue 
    await this.printTwo();        // -> Stack goes within printTwo(), after finished await
    //"for(let i = 0;i < 2e9; i++) {};" and "console.log(1);" -> microtask queue
    for(let i = 0;i < 2e9; i++) {};
    console.log(1);
  }

  async printOneClick(){
    console.log("A");                                 // -> Stack -> 1. A                      
    setTimeout(() => console.log("setTimeout"), 0);   // -> macrotask queue  
    for(let i = 0;i < 2e9; i++) {};                   // -> Stack 
    Promise.resolve().then(() => console.log("Promise.then")); //-> microtask queue 
    await this.printOne();              // -> Stack goes within printOne(), after finished await
                                        // console.log("B"); -> microtask queue
    Promise.resolve().then(() => console.log("Promise.then 2")); // -> microtask queue
    console.log("B"); //already in microtask queue (after await)
  }
  // -> microtask queue execution:
  // 4. Promise.then
  // 5. Promise.then in one
  // 6. 1
  // 7. B
  // 8. Promise.then 2
  // -> macrotask queue execution:
  // 9. setTimeout

  async printAClick(){
    console.log("A");                                                
    setTimeout(() => console.log("setTimeout"), 0);   
    for(let i = 0;i < 2e9; i++) {};    
  }
  //if you click the second time printAClick during first "for(let i = 0;i < 2e9; i++) {};" execution
  //the output will be: 
  // A
  // A
  // setTimeout
  // setTimeout
}

