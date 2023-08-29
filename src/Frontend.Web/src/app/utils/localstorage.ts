export class LocalStorageUtils {

  public getUser() {
    return JSON.parse(localStorage.getItem('affiliate.user'));
  }

  public saveUserData(response: any) {
    this.saveUserToken(response.accessToken);
    this.saveUser(response.userToken);
  }

  public cleanUserData() {
    localStorage.removeItem('affiliate.token');
    localStorage.removeItem('affiliate.user');
  }

  public getUserToken(): string {
    return localStorage.getItem('affiliate.token');
  }

  public saveUserToken(token: string) {
    localStorage.setItem('affiliate.token', token);
  }

  public saveUser(user: string) {
    localStorage.setItem('affiliate.user', JSON.stringify(user));
  }
}
