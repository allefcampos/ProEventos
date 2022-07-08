import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  eventos: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  private getEventos() {
    this.http.get("https://localhost:5001/api/Evento").subscribe(
      (dados) => {
        console.log(dados);

        this.eventos = dados;
      },
      (error) => console.log(error)
    )
  }
}