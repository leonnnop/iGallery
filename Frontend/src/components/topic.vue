<template>
    <el-container style="height: 1200px; border: 1px solid #eee;background-color;background-color: #fafafa;">
        <el-button @click="clickHandler">默认按钮</el-button>
        <!-- <img v-if="this.allow" :src="this.src" alt="test"> -->
        <img :src="this.src" alt="test">
        <input accept="image/*" name="img" id="upload_file" type="file">
        <el-upload action="https://jsonplaceholder.typicode.com/posts/" list-type="picture-card" :on-preview="handlePictureCardPreview"
            :on-remove="handleRemove">
            <i class="el-icon-plus"></i>
        </el-upload>
        <el-dialog :visible.sync="dialogVisible">
            <img width="100%" :src="dialogImageUrl" alt="">
        </el-dialog>
    </el-container>
</template>

<style scoped>
    .navBarWrap {
        position: fixed;
        top: 0;
        z-index: 999;
    }

    .mainContentScroll {
        margin-top: 55px
    }
</style>

<script>
    import SockJS from 'sockjs-client';
    import Stomp from 'stompjs';

    var start = function () {
        // var inc = document.getElementById('incomming');
        var wsImpl = window.WebSocket || window.MozWebSocket;
        // var form = document.getElementById('sendForm');
        // var input = document.getElementById('sendText');

        // inc.innerHTML += "connecting to server ..<br/>";

        // 创建新的websocket新连接端口为7181
        // window.ws = new wsImpl('ws://10.0.1.41:7181/')

        // var connected = false;
        // while (!connected) {
        //     ws.onmessage = function (evt) {
        //         // inc.innerHTML += evt.data + '<br/>';
        //         // inc.innerHTML += '..我收到了<br/>';
        //         console.log('..我收到了')
        //     };

        //     // 当链接对象找到服务端成功对接后，提示正常打开
        //     ws.onopen = function () {
        //         // inc.innerHTML += '.. connection open<br/>';
        //         console.log('.. connection open<br/>')
        //         this.connected = true;
        //     };

        //     // 当链接对象未找找到服务端成功对接后，提示打开失败，别切单项关闭
        //     ws.onclose = function () {
        //         //    inc.innerHTML += '.. connection closed<br/>';
        //         console.log('.. connection closed<br/>')
        //     }
        // }
        // window.ws = new wsImpl('ws://10.0.1.41:7181/')

        // 当数据从服务器服务中心发送后，继续向下运行过程
        // ws.onmessage = function (evt) {
        //     // inc.innerHTML += evt.data + '<br/>';
        //     // inc.innerHTML += '..我收到了<br/>';
        //     console.log('..我收到了')
        // };

        // // 当链接对象找到服务端成功对接后，提示正常打开
        // ws.onopen = function () {
        //     // inc.innerHTML += '.. connection open<br/>';
        //     console.log('.. connection open<br/>')
        // };

        // // 当链接对象未找找到服务端成功对接后，提示打开失败，别切单项关闭
        // ws.onclose = function () {
        //     //    inc.innerHTML += '.. connection closed<br/>';
        //     console.log('.. connection closed<br/>')
        // }

        // form.addEventListener('submit', function (e) {
        //     e.preventDefault();
        //     var val = input.value;
        //     ws.send(val);
        //     input.value = "";
        // });

    }
    window.onload = start;

    export default {

        data() {
            return {
                navBarFixed: false,
                searchInput: '',
                topBarActiveIndex: '1',
                src: '',
                allow: false,
                dialogImageUrl: '',
                dialogVisible: false
            }
        },
        created() {
            // var socket = new SockJS("http://10.0.1.41:7181");

            // //获取 STOMP 子协议的客户端对象
            // var stompClient = Stomp.over(socket);

            // // 向服务器发起websocket连接并发送CONNECT帧
            // stompClient.connect({}, (frame) => {
            //     //console.log('连接成功')
            // })

            // socket.onopen = function () {
            //     print('[*] open', socket.protocol);
            // };
            // socket.onmessage = function (e) {
            //     print('[.] message', e.data);
            // };
            // socket.onclose = function () {
            //     print('[*] close');
            // };
        },
        mounted() {
            window.addEventListener('scroll', this.handleScroll)
        },
        destroyed() {
            window.removeEventListener('scroll', this.handleScroll)
        },
        methods: {
            handleRemove(file, fileList) {
                console.log(file, fileList);
            },
            handlePictureCardPreview(file) {
                this.dialogImageUrl = file.url;
                this.dialogVisible = true;
            },
            clickHandler() {
                console.log('test');

                var file = document.getElementById("upload_file").files[0];
                var formdata1 = new FormData(); // 创建form对象
                formdata1.append('file', file); // 通
                let config = {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                }; //添加请求头
                this.axios.post('http://10.0.1.61:50192/api/values', formdata1, config).then((response) => { //这里的/xapi/upimage为接口
                    console.log(response.data);
                })

            },
            handleTopBarSelect(key, keyPath) {
                console.log('/' + key);

                this.$router.push('/main/' + key);

            },
            handleScroll() {
                var scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
                var offsetTop = document.querySelector('#topBar').offsetTop
                if (scrollTop > offsetTop) {
                    this.navBarFixed = true
                } else {
                    this.navBarFixed = false
                }
            },
        },

    };
</script>