import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search',
  standalone: true
})
export class SearchPipe implements PipeTransform {
  public transform(contacts: any[], text: string): any[] {
    if (!text) return contacts;

    text = text.toLowerCase();

    return contacts.filter(c =>
      c.name.toLowerCase().includes(text) ||
      c.email.toLowerCase().includes(text)
    );
  }
}
