//
//  main.cpp
//  newtest
//  Created by 김아름 on 2022/02/09.
//

#include <iostream>
using namespace std;
int main(void){
    int n, sum=0, cnt=1, d=9, ans = 0;
    cin>>n;
    while(sum+d<n){
        ans+=(cnt*d);
        sum+=d;

        // 다음자리 숫자의 자리수
        cnt++; // 순차적으로 두번째 숫자
        d=d*10; // 다음자리 숫자의 개수
    }
    ans = ans+((n-sum)*cnt);
    cout << ans<<endl;
}
