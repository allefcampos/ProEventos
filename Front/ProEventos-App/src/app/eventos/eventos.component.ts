import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  eventos: any = [];
  eventosFiltrados: any = [];
  isVisible = true;
  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this._filtroLista) : this.eventos;
  }

  filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  private getEventos() {
    this.http.get("https://localhost:5001/api/Evento").subscribe(
      (dados) => {
        this.eventos = dados;
        this.eventosFiltrados = this.eventos;
      },
      (error) => console.log(error)
    )
  }
}
