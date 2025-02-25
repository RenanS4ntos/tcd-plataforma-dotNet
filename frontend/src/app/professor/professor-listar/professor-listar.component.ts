import { Component, OnInit } from '@angular/core';
import { ProfessorService } from '../../services/professor.service';
import Swal from 'sweetalert2';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-professor-listar',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './professor-listar.component.html',
  styleUrl: './professor-listar.component.css'
})
export class ProfessorListarComponent implements OnInit {
  constructor(
    private professorService: ProfessorService,
    private router: Router) { }
  
  ngOnInit(): void {
    //É executado sem que o componente começa a ser inicializado (renderizado)     
    this.listarProfessor();
  }

  filtro: string = '';
  professores: any[] = [];
  professoresFiltrados: any[] = [];

  listarProfessor() {
    this.professorService.listar().subscribe({
      next: response => {
        this.professores = response.dados;
        this.professoresFiltrados = this.professores; 
      },
      error: error => {
        Swal.fire({
          title: 'Sistema Acadêmico', 
          text: error.dados.mensagem, 
          icon: 'error', 
          confirmButtonText: 'OK'
        })   
      }
    })
  }

  filtrarProfessores() {
    const termo = this.filtro.toLowerCase().trim(); // Converte para minúsculas e remove espaços extras
    this.professoresFiltrados = this.professores.filter(professor =>
      professor.nome.toLowerCase().includes(termo) || // Filtra pelo nome
      professor.biografia.toLowerCase().includes(termo) // Filtra pela biografia
    );    
  }  

  editar(id: any) {
    this.router.navigate(['professor/editar', id]); // Redireciona para a página de edição 
  }  
  
  removerProfessor(id: any) {
    this.professorService.remover(id).subscribe({
      next: (response) => {
        Swal.fire({
          title: 'Sistema Acadêmico', 
          text: response.dados.mensagem, 
          icon: 'success', 
          confirmButtonText: 'OK'
        })        
        this.listarProfessor();
      },
      error: error => {
        Swal.fire({
          title: 'Sistema Acadêmico', 
          text: error.dados.mensagem, 
          icon: 'error', 
          confirmButtonText: 'OK'
        })
      }
    });    
  }  
}
