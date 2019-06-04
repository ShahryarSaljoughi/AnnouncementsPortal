import { TeacherDto } from 'src/app/models/teacherDto';

export class Announcement {
    public author: TeacherDto;
    public title: string;
    public text: string;
    public phoneNo: string;
    public persianCreationTime: string;  
    constructor() {    
    }
}