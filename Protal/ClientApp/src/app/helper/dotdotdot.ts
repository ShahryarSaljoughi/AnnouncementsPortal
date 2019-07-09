import { Pipe, PipeTransform } from '@angular/core';
@Pipe({name: 'dotdotdot'})
export class ThreeDotPipe implements PipeTransform {
  transform(text: string, limit: number): string {
    if (text == null) { return null; }
    if (text.length < 60) { return text; }
    return text.slice(0, 60) + '...';
  }
}
