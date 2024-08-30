import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddUserComponent } from './add-user/add-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserService } from './user.service';

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  address: string;
  role: string;
  isActive: boolean;
}


@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  displayedColumns: string[] = ['id', 'firstName', 'email', 'address', 'action'];
  dataSource: User[] = [];

  constructor(public dialog: MatDialog, private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(res => {
      this.dataSource = res;
    });
  }

  addUserDialog() {
    const dialogRef = this.dialog.open(AddUserComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  openEditUserDialog(row: any) {
    console.log(row);
    const dialogRef = this.dialog.open(EditUserComponent,  {
      data: { userDetails: row },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  openViewUserDialog(row: any) {
    console.log(row);
    const dialogRef = this.dialog.open(UserDetailsComponent,  {
      data: { userDetails: row },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

}
