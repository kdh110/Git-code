#카카오 문자와 숫자열 문제
number_list = {"zero":"0", "one":"1", "two":"2", "three":"3", "four":"4", "five":"5", "six":"6", "seven":"7", "eight":"8", "nine":"9"}
#반복문 배열이 숫자만 있다면 [ ] 문자가 있다면 { }사용

def solution(s):
    answer = s

#반복문에 사용할 배열의 변수가 2씩 짝지어 있다면 for 옆의 변수 역시 2개 사용해야함
    for i, num in number_list.items():
        answer = answer.replace(i, num)
    return int(answer)
