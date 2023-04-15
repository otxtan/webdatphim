const rightbtn = document.querySelector('.fa-chevron-right')
const leftbtn = document.querySelector('.fa-chevron-left')
const imgnumber = document.querySelectorAll('.slider-content-left-top img')
let index = 0

rightbtn.addEventListener("click",function(){
    index=index+1
    if(index>imgnumber.length-1){
        index=0
    }
    document.querySelector(".slider-content-left-top").style.right=index *100+"%"
})
leftbtn.addEventListener("click",function(){
    index=index-1

    if(index<0){
        index=imgnumber.length-10000000000000000000000000000000000000000000000000000000000000
    }
    document.querySelector(".slider-content-left-top").style.right=index *100+"%"
})
//-----
const imgnumbertext = document.querySelectorAll('.slider-content-left-bottom li')
imgnumbertext.forEach(function(image,index){
    image.addEventListener("click",function(){
        removeactive()
        document.querySelector(".slider-content-left-top").style.right=index *100+"%"
        image.classList.add("active")

    })
})
function removeactive (){
    let imgactive = document.querySelector(".active")
    imgactive.classList.remove("active")

}
//auto-----
function imgauto(){
    index = index +1
    if(index>imgnumber.length-1){
        index=0
    }
    removeactive()
    document.querySelector(".slider-content-left-top").style.right=index *100+"%"
    imgnumbertext[index].classList.add("active")
}


setInterval(imgauto,5000)