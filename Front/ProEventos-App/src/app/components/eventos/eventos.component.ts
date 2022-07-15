import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '@app/models/evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  modalRef: BsModalRef;

  eventos: Evento[] = [];
  eventosFiltrados: Evento[] = [];

  isVisible = true;
  public eventoId = 0;
  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista
      ? this.filtrarEventos(this._filtroLista)
      : this.eventos;
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
      (evento) =>
        evento.tema.toLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(
    private _eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.getEventos();
  }

  private getEventos() {
    this.spinner.show();

    this._eventoService
      .getEventos()
      .subscribe(
        (eventos: Evento[]) => {
          this.eventos = eventos;
          this.eventosFiltrados = this.eventos;
        },
        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
        }
      )
      .add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef.hide();
    // this.spinner.show();

    this.toastr.success('O Evento foi deletado com Sucesso.', 'Deletado!');

    // this.eventoService
    //   .deleteEvento(this.eventoId)
    //   .subscribe(
    //     (result: any) => {
    //       if (result.message === 'Deletado') {
    //         this.toastr.success(
    //           'O Evento foi deletado com Sucesso.',
    //           'Deletado!'
    //         );
    //         this.carregarEventos();
    //       }
    //     },
    //     (error: any) => {
    //       console.error(error);
    //       this.toastr.error(
    //         `Erro ao tentar deletar o evento ${this.eventoId}`,
    //         'Erro'
    //       );
    //     }
    //   )
    //   .add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef.hide();
  }
}
