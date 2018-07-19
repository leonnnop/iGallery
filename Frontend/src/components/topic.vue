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