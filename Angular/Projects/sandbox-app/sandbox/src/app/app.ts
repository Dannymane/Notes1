import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserInterface } from './modules/main/sandbox/models/user.model';
import { User } from "./modules/main/sandbox/components/user/user";
import { Observable } from '../../node_modules/rxjs/dist/types/index';
import { HttpClient } from '@angular/common/http';
import { interval, firstValueFrom } from 'rxjs';

type Visibility = 'visible' | 'hidden';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, User],
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

  changeCity() {
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

  // async getValueFromApi(value: string) : Promise<Observable<string>> {
  //   for(let i = 0;i < 2e9;i++) {}   
  //   const url = `${this.localTestApiUrl}?returnValue=${encodeURIComponent(value)}`;
  //   const resp = await this.http.get(url, { responseType: 'text' }); 
  //   console.log(`af${value}: getValueFromApi - after await`); 
  //   return resp;
  // }

  async getValueFromApi(value: string): Promise<string> {
    const url = `${this.localTestApiUrl}?returnValue=${encodeURIComponent(value)}`;
    const response = await firstValueFrom(this.http.get(url, { responseType: 'text' }));
    console.log(`response received`);
    return response;
  }

  async asyncFunctionClick(functionId : number){
                                            
    setTimeout(() => console.log(`af${functionId}: setTimeout`), 0);
    console.log(`af${functionId}: A`); 
    // for(let i = 0;i < 2e9;i++) {}  

    const res1 =  await this.getValueFromApi(functionId.toString());
    const res2 =  await this.getValueFromApi(functionId.toString() + `-2`);
    console.log(res1);
    console.log(res2);
       
    console.log(`af${functionId}: B`);  

    // const [res1, res2] = await Promise.all([
    //   firstValueFrom(this.getValueFromApi(functionId.toString())),
    //   firstValueFrom(this.getValueFromApi(functionId.toString() + '-2'))
    // ]);
    
    // console.log(res1, res2);
         
    // Promise.resolve().then(() => console.log(`af${functionId}: Promise.then`));  
    //console.log(`af${functionId}: ` + this.getOne(functionId));
    // await this.fastAwait();
    // await Promise.all([this.getOne(functionId), this.getTwo(functionId)]);                                          
                                      
    // console.log(`af${functionId}: B`);
  }

}

